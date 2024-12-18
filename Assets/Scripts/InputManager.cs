using System;
using UnityEngine;

public class InputManager : SingletonMonoBehaviourBase<InputManager>
{
    public event Action OnLeftMouseDown;
    public event Action OnLeftMouseUp;
    public event Action OnRightMouseDown;
    public event Action OnLeftShiftDown;
    public event Action OnLeftShiftUp;
    public event Action OnInterationButtonDown;
    public event Action OnTalkButtonDown;
    public event Action OnBackButtonDown;
    public event Action OnItemDropButtonDown;
    public event Action OnPauseButtonDown;

    public bool RunButtonValue { get; private set; }

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
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            OnLeftShiftDown?.Invoke();
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            OnLeftShiftUp?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            OnInterationButtonDown?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            OnTalkButtonDown?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            OnItemDropButtonDown?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnPauseButtonDown?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnBackButtonDown?.Invoke();
        }

        RunButtonValue = Input.GetKey(KeyCode.LeftShift);
    }
}
