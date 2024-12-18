using NaughtyAttributes;
using System;
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

    [SerializeField] private float preferSupplierPeeMultiplier = 1.5f;

    [MinMaxSlider(0, 9999)]
    [SerializeField] private Vector2Int maxPeeCountBounds;
    [SerializeField] private float changeMaterialCooldown = 1.0f;

    [SerializeField] private float addMoneyCooldown = 1.0f;

    [SerializeField] private NeedPeePanel needPeePanel;
    [SerializeField] private HumanPreferPanel preferPanel;

    [SerializeField] private Color peeColor;

    [HideInInspector]
    [SerializeField]
    private HumanProvider _humanProvider;
    private HumanContentController _contentController;

    private GameManager _gameManager;
    private PlayerStats _playerStats;

    public readonly ReactiveProperty<PeeState> state = new ReactiveProperty<PeeState>();

    private SupplierRuntimeInfo _preferSupplier;

    private Renderer[] _renderers;
    private readonly List<Material> _defaultMaterials = new();
    private readonly List<Material> _peeMaterials = new();

    private bool _hasOrder;

    private int _peeCount;
    private int _maxPeeCount;

    private float _lastPeeTime;
    private float _lastAddMoneyTime;

#if UNITY_EDITOR
    private void OnValidate()
    {
        _humanProvider = GetComponent<HumanProvider>();
    }
#endif

    private void Awake()
    {
        _gameManager = GameManager.Instance;
        _playerStats = SavesManager.Instance.PlayerStats.Value;

        _contentController = _humanProvider.ContentController;

        _maxPeeCount = UnityEngine.Random.Range(maxPeeCountBounds.x, maxPeeCountBounds.y);
        state.Value = states[0];

        //HideUI();
    }

    private void OnEnable()
    {
        _contentController.ContentSpawned += InitMeshRenderers;
    }

    private void OnDisable()
    {
        _contentController.ContentSpawned -= InitMeshRenderers;
    }

    private void InitMeshRenderers(HumanContent content)
    {
        _renderers = content.GetComponentsInChildren<Renderer>();
        
        foreach (Renderer renderer in _renderers)
        {
            _defaultMaterials.Add(renderer.sharedMaterial);
            var peeMaterial = new Material(renderer.sharedMaterial);
            peeMaterial.color = peeColor;
            _peeMaterials.Add(peeMaterial);
        }
    }

    public void InitOrder()
    {
        ShowUI();

        _preferSupplier = _gameManager.SuppliersManager.GetRandomavailableSupplier();
        preferPanel.SetContext(_preferSupplier);

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

        var typeMultiplier = peeBox.GetTypeMultiplier();
        _peeCount = Mathf.Clamp(_peeCount + (int) (1 * typeMultiplier), 0, _maxPeeCount);
        _lastPeeTime = Time.time;
        var preferMult = peeBox.GetSupplier() == _preferSupplier ? 1.0f : preferSupplierPeeMultiplier;
        var stateMult = state.Value.moneyMultiplier;

        AddMoney(preferMult * stateMult * typeMultiplier);
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
            SetMaterials(false);
        else
            SetMaterials(true);
    }

    private void AddMoney(float mult)
    {
        if (Time.time - _lastAddMoneyTime < addMoneyCooldown) return;

        _playerStats.ChangeMoney((int)(1 * mult));

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

    private void SetMaterials(bool isPee)
    {
        for (int i = 0; i < _renderers.Length; i++)
        {
            Renderer renderer = _renderers[i];
            var material = isPee ? _peeMaterials[i] : _defaultMaterials[i];
            renderer.material = material;
        }
    }
}
