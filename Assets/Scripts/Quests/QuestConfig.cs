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
        public int hintPriority;
    }

    public enum QuestType
    {
        None = 0,
        Run = 1,
        AllYourself = 2,
        FullBottle = 3,
        Earn = 4,
        BuildFirst = 5,
        BuySupplier = 6,
        ZombieDie = 7,
        Day1 = 8,
        Day10 = 9,
        Vershina = 10,
        ToiletTalk = 11,
        Plants = 12,
        OpenDoors = 13
    }

    public QuestConfigData Data;
    public SupplierConfig supplier;
}
