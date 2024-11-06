using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class DayManager : Singleton<DayManager>
{
    public enum DayState
    {
        NeedOpenDoors = 0,
        SpawnProcess = 1,
        NeedCloseDoors = 2,
        NeedEndDay = 3
    }

    [SerializeField] private float allDayDuration = 60 * 10;

    [Space]
    [SerializeField] private Toilet toilet;
    [SerializeField] private Spawner spawner;

    [HideInInspector]
    public readonly ReactiveProperty<DayState> state = new ReactiveProperty<DayState>(DayState.NeedOpenDoors);

    private DoorsManager _doorsManager;

    private float _currentDayTime;

    private IDisposable _stateSubscrition;

    private void Awake()
    {
        _doorsManager = GameManager.Instance.DoorsManager;
    }

    private void Update()
    {
        if(state.Value == DayState.NeedOpenDoors)
        {
            if (_doorsManager.IsAnyDoorHasState(Door.DoorState.Opened))
            {
                state.Value = DayState.SpawnProcess;
            }
        }
        else if(state.Value == DayState.SpawnProcess)
        {
            _currentDayTime = Mathf.Clamp(_currentDayTime + Time.deltaTime, 0.0f, allDayDuration);
            spawner.SetDayValue(_currentDayTime / allDayDuration);

            if(_currentDayTime >= allDayDuration)
            {
                state.Value = DayState.NeedCloseDoors;
            }
        }
        else if(state.Value == DayState.NeedCloseDoors)
        {
            if (_doorsManager.IsAllDoorHasState(Door.DoorState.Closed))
            {
                state.Value = DayState.NeedEndDay;
            }
        }
    }

    private void OnEnable()
    {
        _stateSubscrition = state.Subscribe(OnStateChanged);
    }

    private void OnDisable()
    {
        _stateSubscrition?.Dispose();
        _stateSubscrition = null;
    }

    private void OnStateChanged(DayState newState)
    {
        Debug.Log(newState);

        if(newState == DayState.NeedOpenDoors)
        {
            _currentDayTime = 0;
            _doorsManager.SetDoorsInteractable(true);
        }
        else if(newState == DayState.SpawnProcess)
        {
            spawner.StartSpawn();
        }
        else if(newState == DayState.NeedCloseDoors)
        {
            _doorsManager.SetDoorsInteractable(true);
        }
        else if (newState == DayState.NeedEndDay)
        {
            spawner.StopSpawn();
            toilet.InteractionHandler.SetInteractable(true);

            GameManager.Instance.Data.SetMoney(GameManager.Instance.Data.Money - 100);
        }
    }
}
