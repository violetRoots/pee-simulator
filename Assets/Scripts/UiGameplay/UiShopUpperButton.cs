using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UiShopUpperButton : MonoBehaviour
{
    public bool IsActive { set => _isActive.Value = value; }

    [SerializeField] private Color activeColor;

    [SerializeField] private Button button;
    [SerializeField] private Image image;

    private ReactiveProperty<bool> _isActive = new ReactiveProperty<bool>(false);

    private IDisposable _activationSubscription;

#if UNITY_EDITOR
    private void OnValidate()
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();
    }
#endif

    private void OnEnable()
    {
        _activationSubscription = _isActive.Subscribe(OnActiveChanged);
    }

    private void OnDisable()
    {
        _activationSubscription?.Dispose();
        _activationSubscription = null;
    }

    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }

    public void Subscribe(Action buttonAction)
    {
        button.onClick.AddListener(() => buttonAction?.Invoke());
    }

    public void OnActiveChanged(bool isActive)
    {
        if (isActive)
        {
            image.color = activeColor;
        }
        else
        {
            image.color = Color.white;  
        }
    }
}
