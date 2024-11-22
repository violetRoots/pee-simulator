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
        Run = 2,
        Injured = 3,
    }

    [SerializeField] private float injuredDuration = 3.0f;

    public readonly ReactiveProperty<CharacterState> state = new ReactiveProperty<CharacterState>(CharacterState.Normal);

    private IDisposable _stateSubscriprion;

    [HideInInspector]
    [SerializeField] 
    private CharacterProvider _characterProvider;
    private CharacterDamageController _damageController;

    private InputManager _inputManager;

#if UNITY_EDITOR
    private void OnValidate()
    {
        _characterProvider = GetComponent<CharacterProvider>();
    }
#endif

    private void Awake()
    {
        _inputManager = InputManager.Instance;

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

    private void Update()
    {
        if (state.Value == CharacterState.Injured) return;

        state.Value = _inputManager.RunButtonValue ? CharacterState.Run : CharacterState.Normal;
    }

    private void OnCharacterDamaged(float DamagedTime)
    {
        state.Value = CharacterState.Injured;
    }

    private void OnCharacterStateChanged(CharacterState newState)
    {
        if (newState == CharacterState.Normal)
        {
            
        }
        else if (newState == CharacterState.Run)
        {
            
        }
        else if (newState == CharacterState.Injured)
        {
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
