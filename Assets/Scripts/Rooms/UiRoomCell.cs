using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Common.Localisation;

public class UiRoomCell : MonoBehaviour
{
    [SerializeField] private TranslatedTextMeshPro title;
    [SerializeField] private TranslatedTextMeshPro description;
    [SerializeField] private TranslatedTextMeshPro price;

    [SerializeField] private string pricePattern;

    [SerializeField] private Image icon;

    public void SetContext(RoomConfig room)
    {
        title.SetKey(room.title);
        description.SetKey(room.description);
        price.SetKey(pricePattern, room.price);

        icon.sprite = room.iconSprite;
    }
}
