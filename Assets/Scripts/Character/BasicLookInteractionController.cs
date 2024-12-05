using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFx.Outline;

public class BasicLookInteractionController : MonoBehaviour
{
    public Action onSelect;
    public Action onDeselect;
    public Action onInteract;
    public Action onTalk;

    [SerializeField] private OutlineBehaviour outline;

    private bool _canInteract = true;

#if UNITY_EDITOR
    private void OnValidate()
    {
        outline = GetComponentInChildren<OutlineBehaviour>();
    }
#endif

    private void Awake()
    {
        outline.enabled = false;
    }

    public void Select()
    {
        outline.enabled = true;

        onSelect?.Invoke();
    }

    public void Deselect()
    {
        outline.enabled = false;

        onDeselect?.Invoke();
    }

    public void SetInteractable(bool canInteract)
    {
        _canInteract = canInteract;
    }

    public void OnInteract()
    {
        if (!_canInteract) return;

        onInteract?.Invoke();
    }

    public void OnTalk()
    {
        if (!_canInteract) return;

        onTalk?.Invoke();
    }
}
