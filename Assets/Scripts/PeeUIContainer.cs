using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeeUIContainer : MonoBehaviour
{
    [SerializeField] private float yRotationOffset = 1.0f;

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
        
        var lookTargetPos = lookRotationTarget.position;
        lookTargetPos.y += yRotationOffset;

        var rotation = Quaternion.LookRotation(lookTargetPos - transform.position).eulerAngles;
        transform.rotation = Quaternion.Euler(rotation);
    }
}
