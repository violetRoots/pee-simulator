using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTriggerInteractionController : MonoBehaviour
{
    public Action CustomInteration;

    private InputManager _inputManager;

    private bool _canInteract = true;
    private bool _characterInTrigger;

    private void Awake()
    {
        _inputManager = InputManager.Instance;
    }

    private void SubscripbeInteractions()
    {
        _inputManager.OnInterationButtonDown += OnInteract;
    }

    private void DisposeInteractions()
    {
        _inputManager.OnInterationButtonDown -= OnInteract;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out CharacterProvider characterProvider)) return;

        SubscripbeInteractions();

        _characterInTrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent(out CharacterProvider characterProvider)) return;

        DisposeInteractions();

        _characterInTrigger = false;
    }

    private void OnInteract()
    {
        if (!_characterInTrigger || !_canInteract) return;
        
        CustomInteration?.Invoke();
    }

    public void SetInteractable(bool canInteract)
    {
        _canInteract = canInteract;
    }
}
