using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFx.Outline;

public class BasicLookInteractionController : MonoBehaviour
{
    public Action CustomInteration;
    public Action TalkInteration;

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
    }

    public void Deselect()
    {
        outline.enabled = false;
    }

    public void SetInteractable(bool canInteract)
    {
        _canInteract = canInteract;
    }

    public void OnInteract()
    {
        if (!_canInteract) return;

        CustomInteration?.Invoke();
    }

    public void OnTalk()
    {
        if (!_canInteract) return;

        TalkInteration?.Invoke();
    }
}
