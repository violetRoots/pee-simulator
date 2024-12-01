using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DatabaseManager : SingletonFromResourcesBase<DatabaseManager>
{
    [SerializeField] private SupplierVisualDatabase supplierVisualDatabase;

    public SupplierVisualInfo GetSupplierVisual(string guid)
    {
        var res = supplierVisualDatabase.objects.Where(kvp => kvp.Key == guid).FirstOrDefault();

        if (res == null) return null;

        return res.Value;
    }
}
