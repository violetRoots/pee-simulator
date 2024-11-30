using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Common.Localisation;
using Common.Utils;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace Common.Editor
{
    public class LocalisationSync
    {
        private static LocalisationSyncData _data;
        private static readonly List<string> Downloaded = new List<string>();
        private static bool _isActive;
        private const string MetaColumnNamePrefix = "%";

        [MenuItem("Tools/perelesoq/Sync Localisation %#&L", false, 0)]
        static async void Execute()
        {
            if (_isActive)
            {
	            Debug.Log("<color=red>Localization synchronization task is active. Please, wait, or try to restart Unity Editor.</color>");
	            return;
            }

            _isActive = true;

            if (_data == null)
	            _data = FindData();

            if (_data == null)
            {
	            Debug.LogError("Can't find LocalisationSyncData.");
	            _isActive = false;
	            return;
            }

            if(_data.clearConsole)
	            ClearConsole.ClearLogConsole();

            Downloaded.Clear();

            try
            {
                await SyncAll();
                Parse();
                _isActive = false;
            }
            catch (OperationCanceledException)
            {
	            Debug.Log("<color=yellow>Localization synchronization was canceled.</color>");
	            _isActive = false;
            }
            catch (Exception e)
            {
	            Debug.Log("<color=red>Localization synchronization failed.</color>\n" + e.Message);
	            _isActive = false;
            }
        }

        public static LocalisationSyncData FindData()
        {
            var guids = AssetDatabase.FindAssets("t:"+ typeof(LocalisationSyncData).FullName);
            if (guids.Length == 0)
            {
                Debug.LogError("Can't find LocalisationSyncData.");
                return null;
            }

            return AssetDatabase.LoadAssetAtPath<LocalisationSyncData>(AssetDatabase.GUIDToAssetPath(guids.First()));
        }

        static async Task SyncAll()
        {
	        Debug.Log("<color=yellow>Starting localization download...</color>");
            var urlList = _data.sheetsUrlNames.Select(sheet => string.Format(_data.UrlPattern, _data.tableId, sheet)).ToList();
            var downloadTasksQuery = from url in urlList select DownloadAsync(url);
            var downloadTasks = downloadTasksQuery.ToList();

            while (downloadTasks.Count > 0)
            {
                var finished = await Task.WhenAny(downloadTasks);
                Downloaded.Add(finished.Result);
                downloadTasks.Remove(finished);
                await finished;
            }

            Debug.Log("<color=green>Localization download successfully.</color>");
        }

        static async Task<string> DownloadAsync(string url)
        {
            using (var client = new WebClient())
            {
                var d = await client.DownloadDataTaskAsync(url);
                return Encoding.Default.GetString(d);
            }
        }

		private static void Parse()
		{
			Debug.Log("<color=yellow>Starting localization parsing...</color>");

			var columns = new List<List<string>>();
			var languages = new List<string>();

			foreach (var downloader in Downloaded)
			{
				var text = ReplaceMarkers(downloader);
				var matches = Regex.Matches(text, "\"[\\s\\S]+?\"");

				foreach (Match match in matches)
					text = text.Replace(match.Value, match.Value
					                                      .Replace("\"", null)
					                                      .Replace(",", "[comma]")
					                                      .Replace("\n", "[newline]"));

				var newLines = text.Split(new[] { Environment.NewLine }, StringSplitOptions.None).ToList();
				var newLanguages = newLines.First().Split(',').Select(i => i.Trim()).ToList();

				// meta information
				var metaColumns = new List<int>();
				for (var i = 0; i < newLanguages.Count; i++)
				{
					if(newLanguages[i].StartsWith(MetaColumnNamePrefix))
						metaColumns.Add(i);
				}
				metaColumns.Reverse();

				foreach (var metaColumnId in metaColumns)
					newLanguages.RemoveAt(metaColumnId);

				newLines.Remove(newLines.First());
				newLanguages.Remove(newLanguages.First());

				foreach (var line in newLines)
				{
					var splitted = line.Split(',').ToList();
					foreach (var metaColumn in metaColumns)
						splitted.RemoveAt(metaColumn);
					if(splitted.Count < 1 || string.IsNullOrEmpty(splitted.First()))
						continue;
					columns.Add(splitted);
				}

				if(languages.Count > 0 && languages.Count != newLanguages.Count)
					Debug.LogWarning("The number of languages on the sheets is not the same!");

				languages = languages.Union(newLanguages).ToList();
			}

			for (var i = 0; i < languages.Count; i++)
				languages[i] = languages[i].ToLower();

			_data.translation = new LocalisationSyncData.TranslationData[languages.Count];
			for (var i = 0; i < _data.translation.Length; i++)
			{
				_data.translation[i] = new LocalisationSyncData.TranslationData
				                       {
					                       langKey = languages[i],
					                       localization = new LocalisationSyncData.LocalizationEntryData[columns.Count]
				                       };
			}

			for (var i = 0; i < columns.Count; i++)
			{
				var clearData = columns[i].Select(j => j.Trim()).Select(j => j
				                                                          .Replace("[comma]", ",")
				                                                          .Replace("[newline]", "\n")).ToList();
				for (var j = 0; j < languages.Count; j++)
				{
					_data.GetLanguageData(languages[j]).localization[i].key = clearData.First();// .ClearLocalizationMetaData();
					var text = clearData[j + 1];
					/*if(string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text))
						Debug.Log($"<color=red>Translation for language \"{languages[j]}\" missing translation for key {clearData.First()}!</color>");*/
					_data.GetLanguageData(languages[j]).localization[i].text = text;
				}
			}

			Debug.Log("<color=green>Localization parsed successfully.</color>");

			if (_data.validateAfterSync)
			{
				Debug.Log("<color=yellow>Starting localization validation...</color>");
				foreach (var languageData in _data.translation)
					languageData.Validate();
				Debug.Log("<color=green>Localization validation completed.</color>");
			}

			EditorUtility.SetDirty(_data);
			AssetDatabase.Refresh();
			AssetDatabase.SaveAssets();
		}

		private static string ReplaceMarkers(string text)
		{
			return text.Replace("[Newline]", "\n");
		}
    }
}
#endif

