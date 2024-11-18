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
    [SerializeField] private Button buyButton;

    [TextArea]
    [SerializeField] private string characteristicsPattern;
    [TextArea]
    [SerializeField] private string pricePattern;

    private BottleManager _bottleManager;

    private PeeSupplierConfig _supplier;

    private void Awake()
    {
        _bottleManager = GameManager.Instance.BottleManager;
    }

    private void OnEnable()
    {
        buyButton.onClick.AddListener(OnBuyButtonClicked);
    }

    private void OnDisable()
    {
        buyButton.onClick.RemoveAllListeners();
    }

    public void SetContext(PeeSupplierConfig supplier)
    {
        _supplier = supplier;

        icon.sprite = _supplier.iconSprite;
        title.text = _supplier.title;
        description.text = _supplier.description;
        characteristics.text = string.Format(characteristicsPattern, _supplier.satisfaction, _supplier.causticity, _supplier.satisfaction);
        price.text = string.Format(pricePattern, _supplier.price);
    }

    private void OnBuyButtonClicked()
    {
        _bottleManager.TryBuyBottle(_supplier);
    }
}
