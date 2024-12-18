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
    private QuestsManager _questsManager;

    public void Init()
    {
        _savesManager = SavesManager.Instance;
        _dayManager = DayManager.Instance;
        _questsManager = GameManager.Instance.QuestsManager;

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
        suppliers = suppliers.OrderByDescending(supplier => supplier.Data.visabilityType).ToArray();

        foreach (var supplier in suppliers)
        {
            var supplierInfo = new SupplierRuntimeInfo()
            {
                configData = supplier.Data,
                isAvailable = supplier.Data.visabilityType == SupplierVisabilityType.OnStart,
            };

            runtimeSuppliers.Add(supplierInfo);
        }
    }

    public SupplierRuntimeInfo[] GetSuppliers()
    {
        return runtimeSuppliers.ToArray();
    }

    public SupplierRuntimeInfo[] GetShopSuppliers()
    {
        return runtimeSuppliers.Where(info => info.configData.visabilityType == SupplierVisabilityType.ByShop).ToArray();
    }

    public SupplierRuntimeInfo GetRandomavailableSupplier()
    {
        var availableSuppliers = runtimeSuppliers.Where(info => info.isAvailable).ToArray();
        return availableSuppliers[UnityEngine.Random.Range(0, availableSuppliers.Length)];
    }

    public void SetAvailableByQuest(QuestRuntimeInfo questInfo)
    {
        var supplier = runtimeSuppliers.Where(supplier => supplier.configData.unlockQuestType == questInfo.configData.type)
                                       .FirstOrDefault();


        if (supplier == null) return;

        supplier.isAvailable = true;
        onUpdateSupplierVisability?.Invoke();
    }

    public void SetAvailableByShop(SupplierRuntimeInfo supplierInfo)
    {
        supplierInfo.isAvailable = true;
        onUpdateSupplierVisability?.Invoke();

        _questsManager.ChangeProgressQuest(QuestConfig.QuestType.BuySupplier, 1);
    }

    private void OnPastDay(int daysCount)
    {
        _savesManager.PlayerStats.Value.runtimeSuppliers = runtimeSuppliers;
    }

    
}
