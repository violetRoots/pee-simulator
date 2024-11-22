using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Common.Localisation
{
    [CreateAssetMenu(fileName = "LocaliztionSyncData", menuName = "Configs/Localization", order = 0)]
    public class LocalisationSyncData : ScriptableObject
    {
        [Header("Preferences")]

        /// <summary>
        /// Table id on Google Spreadsheet.
        /// Let's say your table has the following url https://docs.google.com/spreadsheets/d/1RvKY3VE_y5FPhEECCa5dv4F7REJ7rBtGzQg9Z_B_DE4/edit#gid=331980525
        /// So your table id will be "1RvKY3VE_y5FPhEECCa5dv4F7REJ7rBtGzQg9Z_B_DE4" and sheet id will be "331980525" (gid parameter)
        /// </summary>
        [SerializeField] public string tableId;

        /// <summary>
        /// Table sheet contains sheet name and id. First sheet has always zero id. Sheet name is used when saving.
        /// </summary>
        [SerializeField] public string[] sheetsUrlNames = new string[0];

        [SerializeField] public bool validateAfterSync = true;
        [SerializeField] public bool clearConsole = true;

        public readonly string UrlPattern = "https://docs.google.com/spreadsheets/d/{0}/export?format=csv&gid={1}";

        [Header("Data")]
        [SerializeField] public TranslationData[] translation = new TranslationData[0];

        public TranslationData GetLanguageData(string langKey)
        {
            return translation.FirstOrDefault(data => data.langKey == langKey);
        }

        [Serializable]
        public class TranslationData
        {
            public string langKey;
            public LocalizationEntryData[] localization = new LocalizationEntryData[0];

            public void Validate()
            {
                var noDuplicates = new List<string>();
                for (int i = 0; i < localization.Length; i++)
                {
                    for (int j = 0; j < localization.Length; j++)
                    {
                        if (i == j)
                            continue;
                        if (localization[i].key == localization[j].key)
                        {
                            var key = localization[i].key;
                            if(noDuplicates.Contains(key))
                                continue;

                            noDuplicates.Add(key);
                            Debug.LogError(
                                $"Translation file for language {langKey} contains multiple translation by key {key}!");
                        }
                    }
                }
            }
        }

        [Serializable]
        public struct LocalizationEntryData
        {
            [TranslationKeyFormat] public string key;
            public string text;
        }
    }
}