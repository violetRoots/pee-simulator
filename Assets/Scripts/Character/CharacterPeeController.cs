using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class CharacterPeeController : MonoBehaviour
{
    private InputManager _inputManager;

    private CharacterProvider _characterProvider;
    private CharacterInteractionController _characterInteractionController;
    private CharacterWeaponController _characterWeaponController;

    private IDisposable _characterInteractionSubscription;

    private void Awake()
    {
        _inputManager = InputManager.Instance;

        _characterProvider = GameManager.Instance.CharacterProvider;
        _characterInteractionController = _characterProvider.InteractionController;
        _characterWeaponController = _characterProvider.WeaponController;
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
        if (_characterInteractionController.interactionMode.Value != CharacterInteractionController.CharacterInteractionMode.Gameplay) return;

        _characterWeaponController.ActivateWeaponFire();
    }

    private void DeactivatePee()
    {
        _characterWeaponController.DeactivateWeaponFire();
    }

    private void OnInteractionModeChanged(CharacterInteractionController.CharacterInteractionMode mode)
    {
        if (mode != CharacterInteractionController.CharacterInteractionMode.Build) return;

        DeactivatePee();
    }
}
