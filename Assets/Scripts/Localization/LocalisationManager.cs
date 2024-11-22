using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UniRx;
using UnityEngine;

namespace Common.Localisation
{
    public class LocalisationManager : SingletonFromResourcesBase<LocalisationManager>    // TODO SingletonMonoBehaviourBase
    {
        [SerializeField] private LocalisationSyncData data;

        public readonly Dictionary<string, string> LibDictionary = new Dictionary<string, string>();

        public readonly StringReactiveProperty currentLanguage = new StringReactiveProperty(string.Empty);

        private void SyncLibrary(string langKey)
        {
            if (data == null)
                return;

            LibDictionary.Clear();

            foreach (var translation in data.translation)
            {
                if (translation.langKey != langKey)
                    continue;

                foreach (var locData in translation.localization)
                {
                    if (LibDictionary.ContainsKey(locData.key))
                    {
                        Debug.LogError(
                            $"{this}: Translation for language \"{translation.langKey}\" contains multiple translation by key \"{locData.key}\"!");
                        continue;
                    }

                    LibDictionary.Add(locData.key, locData.text);
                }

                break;
            }

            if(LibDictionary.Count < 1)
                Debug.LogWarning($"{this}: Can't find any translation for language \"{langKey}\"!");

            currentLanguage.Value = langKey;
        }

        public void SetLanguage(string langKey)
        {
            SyncLibrary(langKey);
        }
    }

    public static class StringExtension
    {
        public static string Translate(this string key)
        {
            if (key.Equals(string.Empty))
                return key;

            if (LocalisationManager.Instance.LibDictionary.TryGetValue(key, out var result))
            {
#if DEBUG
                if (string.IsNullOrEmpty(result))
                    result = key;
#endif

                return result;
            }
            Debug.LogWarning(
                    $"Translation for language \"{LocalisationManager.Instance.currentLanguage.Value}\" missing translation for key {key}!");
            return key;
        }

        public static string GetMetadataAndClearKey(this string key, out Dictionary<string, string> res)
        {
            res = null;
            if ( ! key.Contains("{") || ! key.Contains("}"))
                return key;
            var from = key.IndexOf("{", StringComparison.Ordinal);
            var meta = key.Substring(from + 1, key.LastIndexOf("}", StringComparison.Ordinal) - from - 1);
            if (meta.Length > 0)
            {
                res = new Dictionary<string, string>();
                foreach (var kvp in meta.Split('&'))
                {
                    var data = kvp.Split('=');
                    if(data.Length < 2)
                        continue;
                    res[data[0]] = data[1];
                }
            }

            key = key.Substring(0, from);
            return key;
        }

        public static string ClearLocalizationMetaData(this string key)
        {
            return RemoveBetween(key, '{', '}');
        }

        public static string RemoveBetween(string s, char begin, char end)
        {
            return new Regex($"\\{begin}.*?\\{end}").Replace(s, string.Empty);
        }
    }
}