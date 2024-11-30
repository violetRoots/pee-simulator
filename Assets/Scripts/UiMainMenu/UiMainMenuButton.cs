using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class UiMainMenuButton : MonoBehaviour
{
    [SerializeField] private Button button;

#if UNITY_EDITOR
    private void OnValidate()
    {
        button = GetComponent<Button>();
    }
#endif

    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }

    public void Subscribe(Action buttonAction)
    {
        button.onClick.AddListener(() => buttonAction?.Invoke());
    }
}
