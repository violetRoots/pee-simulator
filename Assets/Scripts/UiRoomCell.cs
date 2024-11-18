using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiRoomCell : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI price;

    [TextArea]
    [SerializeField] private string pricePattern;

    [SerializeField] private Image icon;

    public void SetContext(RoomConfig room)
    {
        title.text = room.title;
        description.text = room.description;
        price.text = string.Format(pricePattern, room.price);

        icon.sprite = room.iconSprite;
    }
}
