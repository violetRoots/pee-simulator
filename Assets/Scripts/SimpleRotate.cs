using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotate : BaseAutomatePart
{
    [SerializeField] private float speed = 100.0f;

    [SerializeField] private Transform target;

    private void Update()
    {
        if (!IsActive) return;

        target.Rotate(Vector3.up, speed * Time.deltaTime, Space.Self);
    }
}
