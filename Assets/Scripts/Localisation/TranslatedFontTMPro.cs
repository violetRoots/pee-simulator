using System;
using TMPro;
using UniRx;
using UnityEngine;

public class TranslatedFontTMPro : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textField;

    private LanguageManager _languageManager;

    private IDisposable _languageChangeSubscription;

#if UNITY_EDITOR
    private void OnValidate()
    {
        textField = GetComponent<TextMeshProUGUI>();
    }
#endif

    private void Awake()
    {
        _languageManager = LanguageManager.Instance;

        _languageChangeSubscription = _languageManager.currentLanguage.Subscribe(OnLanguageChange);
    }

    private void OnDestroy()
    {
        _languageChangeSubscription?.Dispose();
        _languageChangeSubscription = null;
    }
    private void OnLanguageChange(Language language)
    {
        textField.font = language.font;
    }
}
