using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiMainMenuManager : SingletonFromResourcesBase<UiMainMenuManager>
{
    [SerializeField] private UiMainMenuButton newGameButton;
    [SerializeField] private UiMainMenuButton continueGameButton;
    [SerializeField] private UiMainMenuButton optionsButton;
    [SerializeField] private UiMainMenuButton exitGameButton;

    [Header("Languages")]
    [SerializeField] private UiMainMenuButton russianButton;
    [SerializeField] private UiMainMenuButton englishButton;

    private SaveGameManager _saveGameManager;
    private LoadManager _loadManager;
    private LanguageManager _languageManager;

    private void Awake()
    {
        _saveGameManager = SaveGameManager.Instance;
        _loadManager = LoadManager.Instance;
        _languageManager = LanguageManager.Instance;

        newGameButton.Subscribe(OnNewGameButtonClicked);
        continueGameButton.Subscribe(OnContinueGameButtonClicked);
        optionsButton.Subscribe(OnOptionsButtonClicked);
        exitGameButton.Subscribe(OnExitGameButtonClicked);

        russianButton.Subscribe(OnRussianLanguageButtonClicked);
        englishButton.Subscribe(OnEnglishLanguageButtonClicked);
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

    private void OnRussianLanguageButtonClicked()
    {
        _languageManager.SetRussianLanguage();
    }

    private void OnEnglishLanguageButtonClicked()
    {
        _languageManager.SetEnglishLanguage();
    }

    private void StartGame()
    {
        _loadManager.LoadGameplayScene();
    }
}
