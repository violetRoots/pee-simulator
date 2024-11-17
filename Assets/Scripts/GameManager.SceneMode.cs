using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public partial class GameManager
{
    public enum SceneState
    {
        Gameplay = 0,
        Ui = 1
    }

    public readonly ReactiveProperty<SceneState> sceneState = new ReactiveProperty<SceneState>(SceneState.Gameplay);

    private readonly ReactiveCollection<object> _uiLockers = new ReactiveCollection<object>();

    private IDisposable _addLockerSubscription;
    private IDisposable _removeLockerSubscription;

    private void InitStateSubscription()
    {
        _addLockerSubscription = _uiLockers.ObserveAdd().Subscribe(OnLockerAdd);
        _removeLockerSubscription = _uiLockers.ObserveRemove().Subscribe(OnLockerRemove);
    }

    private void DisposeStateSubscription()
    {
        _addLockerSubscription?.Dispose();
        _addLockerSubscription = null;

        _removeLockerSubscription?.Dispose();
        _removeLockerSubscription = null;
    }

    public void AddLock(object locker)
    {
        _uiLockers.Add(locker);
    }

    public void RemoveLock(object locker)
    {
        _uiLockers.Remove(locker);
    }

    public bool IsGameplayInputEnabled()
    {
        return sceneState.Value == SceneState.Gameplay;
    }

    public bool IsUiInputEnabled()
    {
        return sceneState.Value == SceneState.Ui;
    }

    private void OnLockerAdd(CollectionAddEvent<object> addEvent)
    {
        UpdateState();
    }

    private void OnLockerRemove(CollectionRemoveEvent<object> removeEvent)
    {
        UpdateState();
    }

    private void UpdateState()
    {
        sceneState.Value = _uiLockers.Count == 0 ? SceneState.Gameplay : SceneState.Ui;
    }
}
