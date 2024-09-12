using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedPeePanel : MonoBehaviour
{
    [SerializeField] private Transform lookRotationTarget;

    private void Update()
    {
        if (lookRotationTarget == null) return;

        var newRotation = transform.rotation.eulerAngles;
        newRotation.y = Quaternion.LookRotation(lookRotationTarget.position - transform.position).eulerAngles.y;
        transform.rotation = Quaternion.Euler(newRotation);
    }
}
