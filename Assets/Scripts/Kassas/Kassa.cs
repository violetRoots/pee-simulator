using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kassa : MonoBehaviour
{
    [SerializeField] private Transform firstPositionInOrder;
    [SerializeField] private float randomRadiusMultiplier = 2.5f;

    public Vector3 GetPositionForOrder()
    {
        var randOffset = Random.insideUnitSphere* randomRadiusMultiplier;
        randOffset = new Vector3(randOffset.x, 0.0f, randOffset.z);

        return firstPositionInOrder.position + randOffset;
    }
}
