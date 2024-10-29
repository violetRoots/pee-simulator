using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ExitPointsManager
{
    [SerializeField] private Transform[] exitPoints;

    public Transform GetRandomExitPoint()
    {
        return exitPoints[UnityEngine.Random.Range(0, exitPoints.Length)];
    }
}
