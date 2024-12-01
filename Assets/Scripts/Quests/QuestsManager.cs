using System;
using System.Linq;
using System.Xml.Serialization;
using UniRx;
using UnityEngine;

[Serializable]
public class QuestsManager
{
    public event Action<QuestRuntimeInfo> onQuestProgressUpdated;

    [SerializeField] private QuestConfig[] questConfigs;

    private ReactiveCollection<QuestRuntimeInfo> runtimeQuests;

    private DayManager _dayManager;
    private PlayerStats _playerStats;

    public void Init()
    {
        _dayManager = DayManager.Instance;
        _playerStats = SavesManager.Instance.PlayerStats.Value;

        runtimeQuests = new ReactiveCollection<QuestRuntimeInfo>(_playerStats.runtimeQuests);

        if (runtimeQuests.Count == 0)
        {
            OnlyFirstInit();
        }

        _dayManager.onPastDay += OnPastDay;
    }

    public void Dispose()
    {
        _dayManager.onPastDay -= OnPastDay;
    }

    public QuestRuntimeInfo[] GetQuests()
    {
        return runtimeQuests.ToArray();
    }

    public bool IsQuestFinished(QuestConfig.QuestType type)
    {
        return runtimeQuests.Where(info => info.configData.type == type).FirstOrDefault().isFinished;
    }

    public void ChangeProgressQuest(QuestConfig.QuestType type, float amount)
    {
        var questInfo = runtimeQuests.Where(info => info.configData.type == type).FirstOrDefault();

        if (questInfo == null || questInfo.isFinished) return;

        questInfo.progressValue += amount;

        if (questInfo.progressValue >= questInfo.configData.maxProgress)
            questInfo.isFinished = true;

        onQuestProgressUpdated?.Invoke(questInfo);
    }

    private void OnPastDay()
    {
        _playerStats.runtimeQuests = runtimeQuests.ToList();
    }

    private void OnlyFirstInit()
    {
        foreach (var config in questConfigs)
        {
            var quest = new QuestRuntimeInfo()
            {
                configData = config.Data,
                progressValue = 0,
                isFinished = false
            };

            runtimeQuests.Add(quest);
        }
    }
}
