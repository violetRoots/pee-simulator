using UnityEngine;

public class UiGameplayManager : SingletonMonoBehaviourBase<UiGameplayManager>
{
    [SerializeField] private UiCircleMenu circleMenu;
    [SerializeField] private UiShopView shopView;
    [SerializeField] private ControlsView controlsView;

    [SerializeField] private RectTransform markersContainer;

    private GameManager _gameManager;
    private InputManager _inputManager;

    private void Awake()
    {

        _gameManager = GameManager.Instance;
        _inputManager = InputManager.Instance;

        circleMenu.gameObject.SetActive(false);
        shopView.gameObject.SetActive(false);
        controlsView.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _inputManager.OnLeftShiftDown += ShowCircleMenu;
        _inputManager.OnLeftShiftUp += HideCircleMenu;

        //_inputManager.OnShopButtonDown += ShowShopView;
        _inputManager.OnBackButtonDown += HideShopView;
    }

    private void OnDisable()
    {
        if(_inputManager != null)
        {
            _inputManager.OnLeftShiftDown -= ShowCircleMenu;
            _inputManager.OnLeftShiftUp -= HideCircleMenu;

            //_inputManager.OnShopButtonDown -= ShowShopView;
            _inputManager.OnBackButtonDown -= HideShopView;
        }
    }

    public RectTransform InstatiateMarker(RectTransform markerPrefab)
    {
        return Instantiate(markerPrefab, markersContainer);
    }

    public void ShowControls(string key)
    {
        controlsView.gameObject.SetActive(true);
        controlsView.SetControls(key);
    }

    public void HideControls()
    {
        controlsView.gameObject.SetActive(false);
    }

    private void ShowCircleMenu()
    {
        if (_gameManager.sceneState.Value != GameManager.SceneState.Gameplay) return;

        circleMenu.gameObject.SetActive(true);
    }

    private void HideCircleMenu()
    {
        circleMenu.ApplySelectedCircleItem();
        circleMenu.gameObject.SetActive(false);
    }

    public void ShowShopView()
    {
        shopView.gameObject.SetActive(true);
    }

    private void HideShopView()
    {
        shopView.gameObject.SetActive(false);
    }
}
