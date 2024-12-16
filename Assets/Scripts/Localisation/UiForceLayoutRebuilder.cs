using Common.Localisation;
using System;
using System.Collections;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Common.UI
{
    [DisallowMultipleComponent] [RequireComponent(typeof(RectTransform))]
    public class UiForceLayoutRebuilder : MonoBehaviour
    {
        [SerializeField] private bool rebuildOnLanguageChange = true;
        [SerializeField] private HorizontalOrVerticalLayoutGroup layout;

        private LanguageManager _languageManager;

        private IDisposable _languageSubscription;
        //private IDisposable _inputSubscription;

        private void Awake()
        {
            _languageManager = LanguageManager.Instance;
        }

        private void Start()
        {
            //_inputSubscription = InputManager.Instance.CurrentInputStrategy.Subscribe(strategy => RebuildRequest());

            if (rebuildOnLanguageChange)
                _languageSubscription = _languageManager.currentLanguage.Subscribe(OnLanguageChanged);
            else
                RebuildRequest();
        }

        private void OnEnable()
        {
            RebuildRequest();
        }

        private void OnDestroy()
        {
            _languageSubscription?.Dispose();
            _languageSubscription = null;
            //_inputSubscription?.Dispose();
            //_inputSubscription = null;
            StopAllCoroutines();
        }

        private void OnValidate()
        {
            layout = GetComponent<HorizontalOrVerticalLayoutGroup>();
        }

        private void OnLanguageChanged(Language language)
        {
            RebuildRequest();
        }

        public void RebuildRequest()
        {
            if (layout != null)
            {
                LayoutRebuilder.MarkLayoutForRebuild(transform as RectTransform);
                if (gameObject.activeInHierarchy)
                    StartCoroutine(EndOfFrame());
                else
                    RebuildNow();
            }
            else
                RebuildNow();
        }

        private IEnumerator EndOfFrame()
        {
            yield return new WaitForEndOfFrame();
            RebuildNow();
            yield return null;
        }

        private void RebuildNow()
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);
        }
    }
}