using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiPauseMenu : MonoBehaviour
{
    [SerializeField] private Button continueButton;
    [SerializeField] private Button returnButton;

    private LoadManager _loadManager;
    private GameManager _gameManager;

    private void Awake()
    {
        _loadManager = LoadManager.Instance;
        _gameManager = GameManager.Instance;

        continueButton.onClick.AddListener(OnContinueButtonClicked);
        returnButton.onClick.AddListener(OnReturnButtonClicked);

        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _gameManager.AddLock(this);
    }

    private void OnDisable()
    {
        _gameManager.RemoveLock(this);
    }

    private void OnContinueButtonClicked()
    {
        gameObject.SetActive(false);
    }

    private void OnReturnButtonClicked()
    {
        _loadManager.LoadMainMenuScene();
    }
}
