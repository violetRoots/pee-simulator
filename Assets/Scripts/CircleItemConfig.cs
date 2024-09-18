using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CircleItem", menuName = "Configs/CircleItemConfig", order = 0)]
public class CircleItemConfig : ScriptableObject
{
    public string title;
    public int price;
    public PeeAutomate automate;
}
