using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class HumanFightManager
{
    [Serializable]
    public class HumanFightData
    {
        public HumanType typeA;
        public HumanType typeB;
    }

    public string FightLayer => fightLayer;

    [Layer]
    [SerializeField] private string fightLayer;

    [SerializeField] private HumanFightData[] humanFightDatas;

    [SerializeField] private GameObject[] humanBloodEffects;

    public bool IsTypeStartsFight(HumanType typeA, HumanType typeB)
    {
        var fightData = humanFightDatas.Where(data => data.typeA == typeA && data.typeB == typeB || data.typeA == typeB && data.typeB == typeA).FirstOrDefault();

        return fightData != null;
    }

    public GameObject[] GetBloodEffects()
    {
        return humanBloodEffects;
    }
}
