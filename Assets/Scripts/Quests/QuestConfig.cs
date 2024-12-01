using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "Configs/QuestConfig", order = 2)]
public class QuestConfig : ScriptableObject
{
    [Serializable]
    public class QuestConfigData
    {
        public QuestType type;
        public string title;
        public string description;
        public int maxProgress = 1;
        public string supplierVisualId;
    }

    public enum QuestType
    {
        None = 0,
        Run = 1
    }

    public QuestConfigData Data;
    public SupplierConfig supplier;
}
