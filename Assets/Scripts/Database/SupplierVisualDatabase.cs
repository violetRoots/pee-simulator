using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SupplierVisualDatabase", menuName = "Databases/SupplierVisualDatabase", order = 0)]
public class SupplierVisualDatabase : ScriptableDatabase<SupplierVisualInfo>
{
    
}

[Serializable]
public class SupplierVisualInfo
{
    public Sprite icon;
    public Sprite unknownIcon;
}
