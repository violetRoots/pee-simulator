using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PeeAutomate : MonoBehaviour
{
    [SerializeField] private Vector3 defaultOffset;
 
    [SerializeField] private Material greenPlaceMaterial;
    [SerializeField] private Material redPlaceMaterial;

    [SerializeField] private Collider placeMarker;
    [SerializeField] private BasePeePart[] automateParts;

    [SerializeField] private PeeAutomateTimerPanel automateTimerPanel;
    [SerializeField] private GameObject bottleHint;

    private GameManager _gameManager;
    private GameplaySector _sector;

    private CircleItemConfig _config;

    private MeshRenderer _placeMarkerRenderer;

    private bool _isActivated;
    private float _activatedTime;

    private void Awake()
    {
        _gameManager = GameManager.Instance;

        _placeMarkerRenderer = placeMarker.GetComponent<MeshRenderer>();

        SetActiveUI(false);
    }

    public void Activate(GameplaySector sector, CircleItemConfig circleItemConfig)
    {
        if (sector == null) return;

        _config = circleItemConfig;
        _sector = sector;

        foreach (var part in automateParts)
        {
            if (part is PeeGenerator peeGenerator)
            {
                peeGenerator.Init(sector.BoilerSystem);
            }
        }

        _sector.AutomateSystem.AddAutomate(this);

        SetPlaceMarkerActive(false);
        SetActiveUI(true);

        _isActivated = true;
        _activatedTime = Time.time;
    }

    private void Update()
    {
        if (!_isActivated) return;

        SetCanPee(!_config.needBoiler || _sector.BoilerSystem.HasBottles());

        UpdateTimerLogic();

        UpdateUI();
    }

    public void SetPlaceMarkerActive(bool isActive)
    {
        placeMarker.gameObject.SetActive(isActive);
    }

    public void SetPlaceMarkerMaterial(bool canPlace)
    {
        _placeMarkerRenderer.material = canPlace ? greenPlaceMaterial : redPlaceMaterial;
    }

    public Vector3 GetOffset()
    {
        return defaultOffset;
    }

    private void SetActiveUI(bool isActive)
    {
        automateTimerPanel.gameObject.SetActive(isActive);
    }

    private void UpdateUI()
    {
        var timerValue = Mathf.Clamp01(((_config.duration + _activatedTime) - (Time.time - _activatedTime)) / (_config.duration + _activatedTime));
        automateTimerPanel.UpdateValue(timerValue);
    }

    private void UpdateTimerLogic()
    {
        if (Time.time - _activatedTime >= _activatedTime + _config.duration)
        {
            Destroy(gameObject);
        }
    }

    private void SetCanPee(bool value)
    {
        foreach (var part in automateParts)
        {
            if(value)
                part.Activate();
            else
                part.Deactivate();
        }

        automateTimerPanel.UpdateHint(!value);
    }
}
