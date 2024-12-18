using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

[CreateAssetMenu(fileName = "PeeSupplier", menuName = "Configs/PeeSupplier", order = 1)]
public class SupplierConfig : ScriptableObject
{
    [Serializable]
    public class SupplierConfigData
    {
        public string visualId;
        public string title;
        public string description;

        public int price;

        public int satisfaction = 1;
        public int causticity = 1;
        public int saturation = 1;

        public SupplierVisabilityType visabilityType;

        [Header("Only for 'by quest'")]
        public QuestConfig.QuestType unlockQuestType;
        [Header("Only for 'by shop'")]
        public int suppliersShopPrice;

    }

    public SupplierConfigData Data;
}

public enum SupplierVisabilityType
{
    OnStart = 0,
    ByQuest = 1,
    ByShop = 2,
}
