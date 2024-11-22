using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class InjuredEffectPanel : MonoBehaviour
{
    [SerializeField] private CanvasGroup effectGroup;
    [SerializeField] private float effectFrequencyMultiplier = 5.0f;
    [MinMaxSlider(0.0f, 1.0f)]
    [SerializeField] private Vector2 alphaBounds;

    private GameManager _gameManager;
    private CharacterStateController _characterStateController;

    private bool _isEffectActive;

    private IDisposable _characterStateSubscription;

    private void Awake()
    {
        _gameManager = GameManager.Instance;
        _characterStateController = _gameManager.CharacterProvider.StateController;
    }

    private void OnEnable()
    {
        _characterStateSubscription = _characterStateController.state.Subscribe(OnCharacterStateChanged);
    }

    private void OnDisable()
    {
        _characterStateSubscription?.Dispose();
        _characterStateSubscription = null;
    }

    private void Update()
    {
        if (!_isEffectActive) return;

        var lerpValue = (Mathf.Sin(Time.time * effectFrequencyMultiplier) + 1.0f) * 0.5f;
        effectGroup.alpha = Mathf.Lerp(alphaBounds.x, alphaBounds.y, lerpValue);
    }

    private void OnCharacterStateChanged(CharacterStateController.CharacterState newState)
    {
        if (newState == CharacterStateController.CharacterState.Injured)
        {
            _isEffectActive = true;
        }
        else
        {
            _isEffectActive = false;
        }

        effectGroup.gameObject.SetActive(_isEffectActive);
    }
}
