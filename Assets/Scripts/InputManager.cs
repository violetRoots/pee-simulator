using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    public event Action OnLeftMouseDown;
    public event Action OnLeftMouseUp;
    public event Action OnRightMouseDown;
    public event Action OnLeftShiftDown;
    public event Action OnLeftShiftUp;
    public event Action OnInterationButtonDown;
    public event Action OnShopButtonDown;
    public event Action OnBackButtonDown;
    public event Action OnItemDropButtonDown;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnLeftMouseDown?.Invoke();
        }
        if (Input.GetMouseButtonUp(0))
        {
            OnLeftMouseUp?.Invoke();
        }
        if (Input.GetMouseButtonDown(1))
        {
            OnRightMouseDown?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            OnLeftShiftDown?.Invoke();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            OnLeftShiftUp?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            OnInterationButtonDown?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            OnShopButtonDown?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnBackButtonDown?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            OnItemDropButtonDown?.Invoke();
        }
    }
}
