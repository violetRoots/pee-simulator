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
    }
}
