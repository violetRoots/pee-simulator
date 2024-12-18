using Common.Localisation;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControlsView : MonoBehaviour
{
    [SerializeField] private TranslatedTextMeshPro controls;

    private bool _isLocked = false;

    public void SetControls(string key)
    {
        if(_isLocked) return;

        controls.SetKey(key);
    }

    public void SetLock(bool locked)
    {
        _isLocked = locked;
    }
}
