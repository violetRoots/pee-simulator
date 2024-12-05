using Common.Localisation;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControlsView : MonoBehaviour
{
    //[SerializeField] private TranslatedTextMeshPro controls;
    [SerializeField] private TextMeshProUGUI controls;

    public void SetControls(string key)
    {
        //controls.SetKey(key);
        controls.text = key;
    }
}
