using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiMainMenuManager : SingletonMonoBehaviourBase<UiMainMenuManager>
{
    [SerializeField] private UiMainMenuButton newGameButton;
    [SerializeField] private UiMainMenuButton continueGameButton;
    [SerializeField] private UiMainMenuButton optionsButton;
    [SerializeField] private UiMainMenuButton exitGameButton;

    private SaveGameManager _saveGameManager;
    private LoadManager _loadManager;

    private void Awake()
    {
        _saveGameManager = SaveGameManager.Instance;
        _loadManager = LoadManager.Instance;

        newGameButton.Subscribe(OnNewGameButtonClicked);
        continueGameButton.Subscribe(OnContinueGameButtonClicked);
        optionsButton.Subscribe(OnOptionsButtonClicked);
        exitGameButton.Subscribe(OnExitGameButtonClicked);
    }

    private void OnNewGameButtonClicked()
    {
        _saveGameManager.ClearSaves();

        StartGame();
    }

    private void OnContinueGameButtonClicked()
    {
        StartGame();
    }

    private void OnOptionsButtonClicked()
    {

    }

    private void OnExitGameButtonClicked()
    {
        Application.Quit();
    }

    private void StartGame()
    {
        _loadManager.LoadGameplayScene();
    }
}
