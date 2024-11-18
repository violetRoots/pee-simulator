using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SuppliersManager
{
    public class PeeSupplierRuntimeInfo
    {
        public PeeSupplierConfig config;
    }

    [SerializeField] private PeeSupplierConfig[] suppliers;

    private readonly List<PeeSupplierRuntimeInfo> suppliersInfo = new List<PeeSupplierRuntimeInfo>();

    public void Init()
    {
        foreach (var supplier in suppliers)
        {
            var supplierInfo = new PeeSupplierRuntimeInfo()
            {
                config = supplier
            };

            suppliersInfo.Add(supplierInfo);
        }
    }

    public PeeSupplierRuntimeInfo[] GetAvailableSuppliers()
    {
        return suppliersInfo.ToArray();
    }

    public PeeSupplierRuntimeInfo GetRandomavailableSupplier()
    {
        return suppliersInfo[UnityEngine.Random.Range(0, suppliersInfo.Count)];
    }
}
