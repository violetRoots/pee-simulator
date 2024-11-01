using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

public class HumanPeeController : MonoBehaviour
{
    public float HappyValue => Mathf.Clamp01((float)_peeCount / _maxPeeCount);

    [Serializable]
    public class PeeState
    {
        [Range(0f, 1f)]
        public float happyValue;
        public float moneyMultiplier = 1.0f;

        public HumanEmotionController.HumanEmotion emotion;
    }

    [SerializeField] private PeeState[] states;

    [MinMaxSlider(1, 10)]
    [SerializeField] private Vector2Int maxPeeCountBounds;
    [SerializeField] private int maxPeeMultiplier = 100;
    [SerializeField] private float changeMaterialCooldown = 1.0f;

    [SerializeField] private float addMoneyCooldown = 1.0f;

    [SerializeField] private NeedPeePanel needPeePanel;

    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material peeMaterial;

    [SerializeField] private Renderer[] renderers;

    private GameManager _gameManager;

    public readonly ReactiveProperty<PeeState> state = new ReactiveProperty<PeeState>();

    private bool _hasOrder;

    private int _peeCount;
    private int _maxPeeCount;

    private float _lastPeeTime;
    private float _lastAddMoneyTime;

    private void Awake()
    {
        _gameManager = GameManager.Instance;

        _maxPeeCount = UnityEngine.Random.Range(maxPeeCountBounds.x, maxPeeCountBounds.y) * maxPeeMultiplier;
        state.Value = states[0];

        //HideUI();
    }

    public void InitOrder()
    {
        ShowUI();

        _hasOrder = true;
    }

    private void Update()
    {
        UpdatePeeState();

        //if (!_hasOrder) return; 

        UpdateVisual();
        UpdateUI();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (!collider.TryGetComponent(out PeeBox peeBox)) return;

        _peeCount = Mathf.Clamp(_peeCount + 1, 0, _maxPeeCount);
        _lastPeeTime = Time.time;

        AddMoney();
    }

    private void UpdatePeeState()
    {
        var newState = states.OrderBy(s => s.happyValue).Where(s => s.happyValue - HappyValue >= 0).FirstOrDefault();
        if (newState != null && newState != state.Value)
        {
            state.Value = newState;
        }
    }

    private void UpdateVisual()
    {
        if (Time.time - _lastPeeTime >= changeMaterialCooldown)
            SetMaterials(defaultMaterial);
        else
            SetMaterials(peeMaterial);
    }

    private void AddMoney()
    {
        if (Time.time - _lastAddMoneyTime < addMoneyCooldown) return;

        _gameManager.Data.SetMoney((int)(_gameManager.Data.Money + (1 * state.Value.moneyMultiplier)));

        _lastAddMoneyTime = Time.time;
    }

    private void ShowUI()
    {
        needPeePanel.gameObject.SetActive(true);
    }

    private void UpdateUI()
    {
        needPeePanel.UpdateSlider(HappyValue);
    }

    private void HideUI()
    {
        needPeePanel.gameObject.SetActive(false);
    }

    private void SetMaterials(Material material)
    {
        foreach (var renderer in renderers)
        {
            renderer.material = material;
        }
    }
}
