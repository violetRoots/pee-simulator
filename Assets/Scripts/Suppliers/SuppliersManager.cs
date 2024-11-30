using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SuppliersManager
{
    public class SupplierRuntimeInfo
    {
        public SupplierConfig.SupplierConfigData configData;
    }

    [SerializeField] private SupplierConfig[] suppliers;

    private readonly List<SupplierRuntimeInfo> suppliersInfo = new List<SupplierRuntimeInfo>();

    public void Init()
    {
        foreach (var supplier in suppliers)
        {
            var supplierInfo = new SupplierRuntimeInfo()
            {
                configData = supplier.Data
            };

            suppliersInfo.Add(supplierInfo);
        }
    }

    public SupplierRuntimeInfo[] GetAvailableSuppliers()
    {
        return suppliersInfo.ToArray();
    }

    public SupplierRuntimeInfo GetRandomavailableSupplier()
    {
        return suppliersInfo[UnityEngine.Random.Range(0, suppliersInfo.Count)];
    }
}
