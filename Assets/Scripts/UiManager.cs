using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : Singleton<UiManager>
{
    [SerializeField] private UiCircleMenu circleMenu;

    private GameManager _gameManager;
    private InputManager _inputManager;

    private void Awake()
    {
        _gameManager = GameManager.Instance;
        _inputManager = InputManager.Instance;

        circleMenu.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _inputManager.OnLeftShiftDown += ShowCircleMenu;
        _inputManager.OnLeftShiftUp += HideCircleMenu;
    }

    private void OnDisable()
    {
        if(_inputManager != null)
        {
            _inputManager.OnLeftShiftDown -= ShowCircleMenu;
            _inputManager.OnLeftShiftUp -= HideCircleMenu;
        }
    }

    private void ShowCircleMenu()
    {
        _gameManager.SetSceneState(GameManager.SceneState.Ui);

        circleMenu.gameObject.SetActive(true);
    }

    private void HideCircleMenu()
    {
        _gameManager.SetSceneState(GameManager.SceneState.Gameplay);

        circleMenu.ApplySelectedAutomate();
        circleMenu.gameObject.SetActive(false);
    }
}
