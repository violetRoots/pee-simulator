using Common.Localisation;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageManager : SingletonFromResourcesBase<LanguageManager>
{
    [SerializeField] private Language russianLanguage;
    [SerializeField] private Language englishLanguage;

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

    private void SetLanguage(Language language)
    {
        _localisationManager.SetLanguage(language.languageName);
    }
}
