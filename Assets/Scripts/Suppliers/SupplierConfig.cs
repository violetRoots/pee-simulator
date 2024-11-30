using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

[CreateAssetMenu(fileName = "PeeSupplier", menuName = "Configs/PeeSupplier", order = 1)]
public class SupplierConfig : ScriptableObject
{
    [Serializable]
    public class SupplierConfigData
    {
        public string iconSpriteId;
        public string title;
        public string description;

        public int price;

        public int satisfaction = 1;
        public int causticity = 1;
        public int saturation = 1;
    }

    public SupplierConfigData Data;
}
