using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HumanPeeController : MonoBehaviour
{
    [Serializable]
    public class PeeOriginInfo
    {
        public PeeGenerator generator;
        public float registeredTime;
    }

    [MinMaxSlider(1, 10)]
    [SerializeField] private Vector2Int neededPeeOriginsBounds;
    [SerializeField] private float peeCooldown = 1.0f;
    [SerializeField] private float addMoneyCooldown = 1.0f;
    [SerializeField] private float moneyMultiplier = 1.0f;

    [SerializeField] private NeedPeePanel needPeePanel;

    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material peeMaterial;

    [SerializeField] private Renderer[] renderers;

    private GameManager _gameManager;

    private List<PeeOriginInfo> _peeOriginInfos = new List<PeeOriginInfo>();

    private bool _hasOrder;
    private int _neededPeeOriginsCount;

    private float _lastAddMoneyTime;

    private void Awake()
    {
        _gameManager = GameManager.Instance;

        HideNeedPeePanel();
    }

    private void Update()
    {
        if (!_hasOrder) return; 

        UpdateOriginInfos();
        UpdateNeedPeePanel();
        UpdateMoney();

        if(_peeOriginInfos.Count == 0)
            SetMaterials(defaultMaterial);
        else
            SetMaterials(peeMaterial);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (!collider.TryGetComponent(out PeeBox peeBox)) return;

        if (_peeOriginInfos.Any(info => info.generator == peeBox.Generator)) return;

        _peeOriginInfos.Add( new PeeOriginInfo 
        {  
            generator = peeBox.Generator, 
            registeredTime = Time.time 
        });
    }

    public void InitOrder()
    {
        _neededPeeOriginsCount = UnityEngine.Random.Range(neededPeeOriginsBounds.x, neededPeeOriginsBounds.y);

        ShowNeedPeePanel();

        _hasOrder = true;
    }

    private void UpdateOriginInfos()
    {
        _peeOriginInfos = _peeOriginInfos.Where(info => Time.time - info.registeredTime < peeCooldown).ToList();
    }

    private void UpdateNeedPeePanel()
    {
        needPeePanel.SetCount(_neededPeeOriginsCount - _peeOriginInfos.Count);
    }

    private void ShowNeedPeePanel()
    {
        needPeePanel.gameObject.SetActive(true);
    }

    private void HideNeedPeePanel()
    {
        needPeePanel.gameObject.SetActive(false);
    }

    private void UpdateMoney()
    {
        if (_peeOriginInfos.Count < _neededPeeOriginsCount) return;

        if (Time.time - _lastAddMoneyTime < addMoneyCooldown) return;

        _gameManager.Data.SetMoney((int)(_gameManager.Data.Money + (1 * moneyMultiplier)));

        _lastAddMoneyTime = Time.time;
    }

    private void SetMaterials(Material material)
    {
        foreach(var renderer in renderers)
        {
            renderer.material = material;
        }
    }
}
