using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class CharacterInteractionController : MonoBehaviour
{
    public enum CharacterInteractionMode
    {
        Gameplay = 0,
        Build = 1,
        CarryItem = 2,
    }

    public readonly ReactiveProperty<CharacterInteractionMode> interactionMode = new ReactiveProperty<CharacterInteractionMode>(CharacterInteractionMode.Gameplay);

    private GameManager _gameManager;
    private InputManager _inputManager;

    private void Awake()
    {
        _gameManager = GameManager.Instance;
        _inputManager = InputManager.Instance;
    }

    private void OnEnable()
    {
        _inputManager.OnRightMouseDown += SetToDefaultMode;
    }

    private void OnDisable()
    {
        if (_inputManager == null) return;

        _inputManager.OnRightMouseDown -= SetToDefaultMode;
    }

    public void SetInteractionMode(CharacterInteractionMode mode)
    {
        interactionMode.Value = mode;
    }

    private void SetToDefaultMode()
    {
        if (!_gameManager.IsGameplayInputEnabled()) return;

        if (interactionMode.Value != CharacterInteractionMode.Build) return;

        interactionMode.Value = CharacterInteractionMode.Gameplay;
    }
}
