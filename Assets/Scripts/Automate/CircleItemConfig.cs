using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CircleItem", menuName = "Configs/CircleItemConfig", order = 0)]
public class CircleItemConfig : ScriptableObject
{
    public string title;
    public int price = 0;
    public float duration = 10.0f;
    public PeeAutomate automate;
}
