using System;
using System.Linq;
using UniRx;
using UnityEngine;

[Serializable]
public class QuestsManager
{
    [Serializable]
    public class QuestRuntimeInfo
    {
        public QuestConfig.QuestConfigData configData;
        public float progressValue = 0;
        public bool isFinished;
    }

    public event Action<QuestConfig.QuestType> onQuestProgressUpdated;

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

    public QuestRuntimeInfo[] GetUnfinishedQuests()
    {
        return runtimeQuests.Where(info => !info.isFinished).ToArray();
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

        onQuestProgressUpdated?.Invoke(type);
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
