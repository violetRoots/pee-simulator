using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SuppliersManager;

[Serializable]
public class QuestsManager
{
    public class QuestRuntimeInfo
    {
        public QuestConfig config;
        public float progressValue;
        public bool isComeplete;
    }

    [SerializeField] private QuestConfig[] quests;

    private readonly List<QuestRuntimeInfo> questsInfo = new List<QuestRuntimeInfo>();

    public void Init()
    {
        foreach (var quest in quests)
        {
            var questInfo = new QuestRuntimeInfo()
            {
                config = quest
            };

            questsInfo.Add(questInfo);
        }
    }

    public QuestRuntimeInfo[] GetQuests()
    {
        return questsInfo.ToArray();
    }
}
