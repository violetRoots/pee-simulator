using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ZombiePointsManager
{
    [SerializeField] private Transform[] zombiePoints;

    public Transform GetRandomPoint()
    {
        var index = UnityEngine.Random.Range(0, zombiePoints.Length);
        return zombiePoints[index];
    }
}
