using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

public class UiChecksPanel : MonoBehaviour
{
    [SerializeField] private UiCheckCell checkCell;
    [SerializeField] RectTransform checksContentObj;

    private ChecksManager _checksManager;

    private readonly List<UiCheckCell> _checkCells = new();

    private IDisposable _checksAddSubscription;
    private IDisposable _checksRemoveSubscription;

    private void Awake()
    {
        _checksManager = GameManager.Instance.ChecksManager;

        _checksAddSubscription = _checksManager.runtimeChecks.ObserveAdd().Subscribe(OnNewCheckAdded);
        _checksRemoveSubscription = _checksManager.runtimeChecks.ObserveRemove().Subscribe(OnNewCheckRemoved);
        InitChecks();
    }

    private void OnDestroy()
    {
        _checksAddSubscription?.Dispose();
        _checksAddSubscription = null;

        _checksRemoveSubscription?.Dispose();
        _checksRemoveSubscription = null;
    }

    private void InitChecks()
    {
        foreach (var checkInfo in _checksManager.runtimeChecks)
        {
            AddCheckCell(checkInfo);
        }
    }

    private void OnNewCheckAdded(CollectionAddEvent<ChecksManager.CheckRuntimeInfo> e)
    {
        AddCheckCell(e.Value);
    }

    private void OnNewCheckRemoved(CollectionRemoveEvent<ChecksManager.CheckRuntimeInfo> e)
    {
        var cellToRemove = _checkCells.Where(cell => cell.RuntimeInfo.configData == e.Value.configData).FirstOrDefault();

        if (cellToRemove == null) return;

        _checkCells.Remove(cellToRemove);
        Destroy(cellToRemove.gameObject);
    }

    private void AddCheckCell(ChecksManager.CheckRuntimeInfo info)
    {
        var cell = Instantiate(checkCell, checksContentObj);

        cell.SetContext(info, () => _checksManager.PayCheck(info));

        _checkCells.Add(cell);
    }
}
