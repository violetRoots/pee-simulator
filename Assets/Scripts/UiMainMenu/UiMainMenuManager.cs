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
    [SerializeField] private UiMainMenuButton chineseButton;
    [SerializeField] private UiMainMenuButton spanishButton;
    [SerializeField] private UiMainMenuButton portugueseButton;
    [SerializeField] private UiMainMenuButton germanButton;
    [SerializeField] private UiMainMenuButton japaneseButton;
    [SerializeField] private UiMainMenuButton frenchButton;

    private SavesManager _dataManager;
    private LoadManager _loadManager;
    private LanguageManager _languageManager;
    private AudioManager _audioManager;

    private void Awake()
    {
        _dataManager = SavesManager.Instance;
        _loadManager = LoadManager.Instance;
        _languageManager = LanguageManager.Instance;

        newGameButton.Subscribe(OnNewGameButtonClicked);
        continueGameButton.Subscribe(OnContinueGameButtonClicked);
        optionsButton.Subscribe(OnOptionsButtonClicked);
        exitGameButton.Subscribe(OnExitGameButtonClicked);

        russianButton.Subscribe(OnRussianLanguageButtonClicked);
        englishButton.Subscribe(OnEnglishLanguageButtonClicked);
        chineseButton.Subscribe(OnChineseLanguageButtonClicked);
        spanishButton.Subscribe(OnSpanishLanguageButtonClicked);
        portugueseButton.Subscribe(OnPortugueseLanguageButtonClicked);
        germanButton.Subscribe(OnGermanLanguageButtonClicked);
        japaneseButton.Subscribe(OnJapaneseLanguageButtonClicked);
        frenchButton.Subscribe(OnFrenchLanguageButtonClicked);

        _audioManager = AudioManager.Instance;
        _audioManager.PlayCalmMusic();
    }

    private void OnNewGameButtonClicked()
    {
        _dataManager.PlayerStats.Clear();

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

    private void OnChineseLanguageButtonClicked()
    {
        _languageManager.SetChineseLanguage();
    }

    private void OnSpanishLanguageButtonClicked()
    {
        _languageManager.SetSpanishLanguage();
    }

    private void OnPortugueseLanguageButtonClicked()
    {
        _languageManager.SetPortugueseLanguage();
    }

    private void OnGermanLanguageButtonClicked()
    {
        _languageManager.SetGermanLanguage();
    }

    private void OnJapaneseLanguageButtonClicked()
    {
        _languageManager.SetJapaneseLanguage();
    }

    private void OnFrenchLanguageButtonClicked()
    {
        _languageManager.SetFrenchLanguage();
    }

    private void StartGame()
    {
        _loadManager.LoadGameplayScene();
    }
}
