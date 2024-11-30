using Common.Localisation;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageManager : SingletonFromResourcesBase<LanguageManager>
{
    [SerializeField] private Language russianLanguage;
    [SerializeField] private Language englishLanguage;
    [SerializeField] private Language chineseLanguage;
    [SerializeField] private Language spanishLanguage;
    [SerializeField] private Language portugueseLanguage;
    [SerializeField] private Language germanLanguage;
    [SerializeField] private Language japaneseLanguage;
    [SerializeField] private Language frenchLanguage;

    private LocalisationManager _localisationManager;

    private void Awake()
    {
        _localisationManager = LocalisationManager.Instance;
    }

    public void SetRussianLanguage()
    {
        SetLanguage(russianLanguage);
    }

    public void SetEnglishLanguage()
    {
        SetLanguage(englishLanguage);
    }

    public void SetChineseLanguage()
    {
        SetLanguage(chineseLanguage);
    }

    public void SetSpanishLanguage()
    {
        SetLanguage(spanishLanguage);
    }

    public void SetPortugueseLanguage()
    {
        SetLanguage(portugueseLanguage);
    }

    public void SetGermanLanguage()
    {
        SetLanguage(germanLanguage);
    }

    public void SetJapaneseLanguage()
    {
        SetLanguage(japaneseLanguage);
    }

    public void SetFrenchLanguage()
    {
        SetLanguage(frenchLanguage);
    }

    private void SetLanguage(Language language)
    {
        _localisationManager.SetLanguage(language.languageName);
    }
}
