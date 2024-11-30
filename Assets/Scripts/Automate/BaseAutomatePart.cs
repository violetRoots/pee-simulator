using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAutomatePart : MonoBehaviour
{
    protected bool IsActive { get; set; }

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }
}
