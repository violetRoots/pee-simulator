using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotate : MonoBehaviour
{
    [SerializeField] private float speed = 100.0f;

    [SerializeField] private Transform target;

    private void Update()
    {
        target.Rotate(Vector3.up, speed * Time.deltaTime, Space.Self);
    }
}
