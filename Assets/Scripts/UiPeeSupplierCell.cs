using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiPeeSupplierCell : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI characteristics;
    [SerializeField] private TextMeshProUGUI price;

    [TextArea]
    [SerializeField] private string characteristicsPattern;
    [TextArea]
    [SerializeField] private string pricePattern;

    public void SetContext(PeeSupplierConfig supplier)
    {
        icon.sprite = supplier.iconSprite;
        title.text = supplier.title;
        description.text = supplier.description;
        characteristics.text = string.Format(characteristicsPattern, supplier.satisfaction, supplier.causticity, supplier.satisfaction);
        price.text = string.Format(pricePattern, supplier.price);
    }
}
