using UnityEngine;

public class UiGameplayManager : SingletonMonoBehaviourBase<UiGameplayManager>
{
    [SerializeField] private UiCircleMenu circleMenu;
    [SerializeField] private UiShopView shopView;
    [SerializeField] private ControlsView controlsView;
    [SerializeField] private UiTutorialView tutorialView;
    [SerializeField] private UiPauseMenu pauseMenu;

    [SerializeField] private RectTransform markersContainer;

    private GameManager _gameManager;
    private InputManager _inputManager;
    private PlayerStats _playerStats;

    private void Awake()
    {
        _gameManager = GameManager.Instance;
        _inputManager = InputManager.Instance;
        _playerStats = SavesManager.Instance.PlayerStats.Value;

        circleMenu.gameObject.SetActive(false);
        shopView.gameObject.SetActive(false);
        controlsView.gameObject.SetActive(false);

        if(_playerStats.firstLoad)
        {
            tutorialView.gameObject.SetActive(true);
            _playerStats.firstLoad = false;
        }
        else
        {
            tutorialView.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        _inputManager.OnLeftShiftDown += ShowCircleMenu;
        _inputManager.OnLeftShiftUp += HideCircleMenu;

        //_inputManager.OnShopButtonDown += ShowShopView;
        _inputManager.OnBackButtonDown += HideShopView;

        _inputManager.OnPauseButtonDown += OnPauseButtonDown;
    }

    private void OnDisable()
    {
        if(_inputManager != null)
        {
            _inputManager.OnLeftShiftDown -= ShowCircleMenu;
            _inputManager.OnLeftShiftUp -= HideCircleMenu;

            //_inputManager.OnShopButtonDown -= ShowShopView;
            _inputManager.OnBackButtonDown -= HideShopView;

            _inputManager.OnPauseButtonDown -= OnPauseButtonDown;
        }
    }

    public RectTransform InstatiateMarker(RectTransform markerPrefab)
    {
        return Instantiate(markerPrefab, markersContainer);
    }

    public void ShowControls(string key, bool lockMode = false)
    {
        controlsView.gameObject.SetActive(true);
        controlsView.SetControls(key);

        if (lockMode)
            controlsView.SetLock(true);
    }

    public void HideControls(bool lockMode = false)
    {
        controlsView.gameObject.SetActive(false);

        if (lockMode)
            controlsView.SetLock(false);
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

    public void SetCircleItemUsed(CircleItemConfig config)
    {
        circleMenu.SetUsed(config);
    }

    private void OnPauseButtonDown()
    {
        if (shopView.gameObject.activeInHierarchy) return;

        pauseMenu.gameObject.SetActive(!pauseMenu.gameObject.activeSelf);
    }
}
