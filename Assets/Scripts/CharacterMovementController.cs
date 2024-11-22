using CMF;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class CharacterMovementController : MonoBehaviour
{
    [SerializeField] private float normalSpeed = 7.0f;
    [SerializeField] private float runSpeed = 10.0f;
    [SerializeField] private float injuredSpeed = 4.0f;
    
    [SerializeField] private AdvancedWalkerController walker;

    [HideInInspector]
    [SerializeField]
    private CharacterProvider _characterProvider;
    private CharacterStateController _stateController;

    private InputManager _inputManager;

    private IDisposable _stateSubscription;

#if UNITY_EDITOR
    private void OnValidate()
    {
        _characterProvider = GetComponent<CharacterProvider>();
    }
#endif

    private void Awake()
    {
        _inputManager = GetComponent<InputManager>();

        _stateController = _characterProvider.StateController;

        SetNormalSpeed();
    }

    private void OnEnable()
    {
        _stateSubscription = _stateController.state.Subscribe(OnCharacterStateChanged);
    }

    private void OnDisable()
    {
        _stateSubscription?.Dispose();
        _stateSubscription = null;
    }

    private void OnCharacterStateChanged(CharacterStateController.CharacterState newState)
    {
        if(newState == CharacterStateController.CharacterState.Normal)
        {
            SetSpeed(normalSpeed);
        }
        else if(newState == CharacterStateController.CharacterState.Run)
        {
            SetSpeed(runSpeed);
        }
        else if (newState == CharacterStateController.CharacterState.Injured)
        {
            SetSpeed(injuredSpeed);
        }
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
