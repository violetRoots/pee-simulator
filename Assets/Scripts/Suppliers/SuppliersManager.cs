using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class SuppliersManager
{
    public event Action onUpdateSupplierVisability;

    [SerializeField] private SupplierConfig[] suppliers;

    private List<SupplierRuntimeInfo> runtimeSuppliers;

    private SavesManager _savesManager;
    private DayManager _dayManager;

    public void Init()
    {
        _savesManager = SavesManager.Instance;
        _dayManager = DayManager.Instance;

        runtimeSuppliers = _savesManager.PlayerStats.Value.runtimeSuppliers;

        if(runtimeSuppliers.Count == 0)
        {
            OnlyFirstInitSuppliers();
        }

        _dayManager.onPastDay += OnPastDay;
    }

    public void Dispose()
    {
        _dayManager.onPastDay -= OnPastDay;
    }

    private void OnlyFirstInitSuppliers()
    {
        suppliers = suppliers.OrderByDescending(supplier => supplier.Data.isAvailableOnStart).ToArray();

        foreach (var supplier in suppliers)
        {
            var supplierInfo = new SupplierRuntimeInfo()
            {
                configData = supplier.Data,
                isAvailable = supplier.Data.isAvailableOnStart
            };

            runtimeSuppliers.Add(supplierInfo);
        }
    }

    public SupplierRuntimeInfo[] GetSuppliers()
    {
        return runtimeSuppliers.ToArray();
    }

    public SupplierRuntimeInfo GetRandomavailableSupplier()
    {
        return runtimeSuppliers[UnityEngine.Random.Range(0, runtimeSuppliers.Count)];
    }

    public void SetAvailableByQuest(QuestRuntimeInfo questInfo)
    {
        var supplier = runtimeSuppliers.Where(supplier => supplier.configData.unlockQuestType == questInfo.configData.type)
                                       .FirstOrDefault();


        if (supplier == null) return;

        supplier.isAvailable = true;
        onUpdateSupplierVisability?.Invoke();
    }

    private void OnPastDay()
    {
        _savesManager.PlayerStats.Value.runtimeSuppliers = runtimeSuppliers;
    }
}
