using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUIContainer : MonoBehaviour
{
    private GameManager _gameManager;

    private Transform lookRotationTarget;

    private void Awake()
    {
        _gameManager = GameManager.Instance;

        lookRotationTarget = _gameManager.CharacterProvider.transform;
    }

    private void Update()
    {
        if (lookRotationTarget == null) return;

        var yRot = Quaternion.LookRotation(lookRotationTarget.position - transform.position).eulerAngles.y;
        transform.rotation = Quaternion.Euler(new Vector3(0, yRot, 0));
    }
}
