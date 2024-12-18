using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePeePart : MonoBehaviour
{
    protected bool IsActive { get; set; }

    public void Activate()
    {
        IsActive = true;

        OnActivated();
    }

    protected virtual void OnActivated() { }

    public void Deactivate()
    {
        IsActive = false;

        OnDeactivated();
    }

    protected virtual void OnDeactivated() { }
}
