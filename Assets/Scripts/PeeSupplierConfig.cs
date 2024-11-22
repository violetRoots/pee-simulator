using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PeeSupplier", menuName = "Configs/PeeSupplier", order = 1)]
public class PeeSupplierConfig : ScriptableObject
{
    public Sprite iconSprite;
    public string title;
    public string description;

    public int price;

    public int satisfaction = 1;
    public int causticity = 1;
    public int saturation = 1;
}
