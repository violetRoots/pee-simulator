using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Room", menuName = "Configs/RoomConfig", order = 4)]
public class RoomConfig : ScriptableObject
{
    public string title;
    [TextArea]
    public string description;
    public Sprite iconSprite;

    public int price;
}
