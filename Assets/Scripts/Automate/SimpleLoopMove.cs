using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleLoopMove : BasePeePart
{
    [SerializeField] private float speed = 1.0f;

    [Space]
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;

    [Space]
    [SerializeField] private Transform target;

    private void Update()
    {
        if (!IsActive) return;

        target.position = Vector3.Lerp(pointA.position, pointB.position, Mathf.PingPong(Time.time / speed, 1));
    }
}
