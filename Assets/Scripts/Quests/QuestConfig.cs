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
        public SupplierConfig.SupplierConfigData supplierData;
        public string title;
        public string description;
        public int maxProgress = 1;
    }

    public enum QuestType
    {
        Run = 0
    }

    public QuestConfigData Data;
    public SupplierConfig supplier;

    private void OnValidate()
    {
        Data.supplierData = supplier.Data;
    }
}
