using CMF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementController : MonoBehaviour
{
    [SerializeField] private float normalSpeed = 7.0f;
    [SerializeField] private float injuredSpeed = 4.0f;
    
    [SerializeField] private AdvancedWalkerController walker;

    private void Awake()
    {
        SetNormalSpeed();
    }

    public void SetNormalSpeed()
    {
        SetSpeed(normalSpeed);
    }

    public void SetInjuredSpeed()
    {
        SetSpeed(injuredSpeed);
    }

    public void SetSpeed(float speed)
    {
        walker.movementSpeed = speed;
    }
}
