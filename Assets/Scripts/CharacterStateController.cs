using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class CharacterStateController : MonoBehaviour
{
    public enum CharacterState
    {
        Normal = 1,
        Injured = 2,
    }

    [SerializeField] private float injuredDuration = 3.0f;

    public readonly ReactiveProperty<CharacterState> state = new ReactiveProperty<CharacterState>(CharacterState.Normal);

    private IDisposable _stateSubscriprion;

    [HideInInspector]
    [SerializeField] 
    private CharacterProvider _characterProvider;
    private CharacterMovementController _movementController;
    private CharacterDamageController _damageController;

#if UNITY_EDITOR
    private void OnValidate()
    {
        _characterProvider = GetComponent<CharacterProvider>();
    }
#endif

    private void Awake()
    {
        _movementController = _characterProvider.MovementController;
        _damageController = _characterProvider.DamageController;
    }

    private void OnEnable()
    {
        _stateSubscriprion = state.Subscribe(OnCharacterStateChanged);
        _damageController.Damaged += OnCharacterDamaged;
    }

    private void OnDisable()
    {
        _stateSubscriprion?.Dispose();
        _stateSubscriprion = null;
    }

    private void OnCharacterDamaged(float DamagedTime)
    {
        Debug.Log("DAMAGE");
        state.Value = CharacterState.Injured;
    }

    private void OnCharacterStateChanged(CharacterState newState)
    {
        if (newState == CharacterState.Normal)
        {
            _movementController.SetNormalSpeed();
        }
        else if (newState == CharacterState.Injured)
        {
            _movementController.SetInjuredSpeed();
            DelayedAction(injuredDuration, () => state.Value = CharacterState.Normal);
        }
    }

    private void DelayedAction(float time, Action action)
    {
        StartCoroutine(DelayedActionProcess(time, action));
    }

    private IEnumerator DelayedActionProcess(float time, Action action)
    {
        yield return new WaitForSeconds(time);

        action?.Invoke();
    }
}
