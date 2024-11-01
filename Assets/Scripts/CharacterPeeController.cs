using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class CharacterPeeController : MonoBehaviour
{
    [SerializeField] private PeeGenerator peeGenerator;

    private InputManager _inputManager;
    private CharacterInteractionController _characterInteractionController;

    private IDisposable _characterInteractionSubscription;

    private void Awake()
    {
        _inputManager = InputManager.Instance;
        _characterInteractionController = GameManager.Instance.CharacterProvider.InteractionController;
    }

    private void OnEnable()
    {
        _inputManager.OnLeftMouseDown += ActivatePee;
        _inputManager.OnLeftMouseUp += DeactivatePee;

        _characterInteractionSubscription = _characterInteractionController.interactionMode.Subscribe(OnInteractionModeChanged);
    }

    private void OnDisable()
    {
        if (_inputManager != null)
        {
            _inputManager.OnLeftMouseDown -= ActivatePee;
            _inputManager.OnLeftMouseUp -= DeactivatePee;
        }

        _characterInteractionSubscription?.Dispose();
        _characterInteractionSubscription = null;
    }

    private void ActivatePee()
    {
        if (_characterInteractionController.interactionMode.Value != CharacterInteractionController.CharacterInteractionMode.Default) return;

        peeGenerator.Activate();
    }

    private void DeactivatePee()
    {
        peeGenerator.Deactivate();
    }

    private void OnInteractionModeChanged(CharacterInteractionController.CharacterInteractionMode mode)
    {
        if (mode != CharacterInteractionController.CharacterInteractionMode.Build) return;

        DeactivatePee();
    }
}
