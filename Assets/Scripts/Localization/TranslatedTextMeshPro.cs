using System;
using System.Linq;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Common.Localisation
{
    [RequireComponent(typeof(TMP_Text))]
    public class TranslatedTextMeshPro : MonoBehaviour
    {
        [SerializeField] private string key;
        [SerializeField] private int[] values;
        [SerializeField][HideInInspector] private RectTransform rectTransform;

        [FormerlySerializedAs("text")] [SerializeField] [HideInInspector]
        private TMP_Text textfield;

        private IDisposable _languageChangeSubscription;

        private TMP_Text Textfield
        {
            get
            {
                if (textfield == null)
                    textfield = GetComponent<TMP_Text>();
                return textfield;
            }
        }

        private void Start()
        {
            if (Textfield == null)
                return;

            _languageChangeSubscription = LocalisationManager.Instance.currentLanguage.Subscribe(OnLanguageChange);
        }

        private void OnDestroy()
        {
            _languageChangeSubscription?.Dispose();
            _languageChangeSubscription = null;
        }

        private void OnValidate()
        {
            rectTransform = GetComponent<RectTransform>();
            var init = Textfield;
            if (Textfield != null && !string.IsNullOrEmpty(key))
                Textfield.text = key;
        }

        private void OnLanguageChange(string lang)
        {
            if (string.IsNullOrEmpty(lang))
                return;

            var newText = values.Length == 0 ? key.Translate() : string.Format(key.Translate(), values.Select(x => x.ToString()).ToArray());

            //var needsRebuild = newText.Length != textfield.text.Length;
            textfield.text = newText;
            if (rectTransform != null)
                LayoutRebuilder.MarkLayoutForRebuild(rectTransform);
        }

        public void SetKey(string value, params int[] values)
        {
            //if(key == value)
            //    return;

            key = value;
            this.values = values;

            if (_languageChangeSubscription != null)
                OnLanguageChange(LocalisationManager.Instance.currentLanguage.Value);
        }
    }
}