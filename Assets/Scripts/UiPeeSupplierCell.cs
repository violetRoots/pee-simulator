using Common.Localisation;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiPeeSupplierCell : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TranslatedTextMeshPro title;
    [SerializeField] private TranslatedTextMeshPro description;
    [SerializeField] private TranslatedTextMeshPro characteristics;
    [SerializeField] private TranslatedTextMeshPro price;
    [SerializeField] private Button buyButton;

    [SerializeField] private string characteristicsPattern;
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
        title.SetKey(_supplier.title);
        description.SetKey(_supplier.description);
        characteristics.SetKey(characteristicsPattern, _supplier.satisfaction, _supplier.causticity, _supplier.satisfaction);
        price.SetKey(pricePattern, _supplier.price);
    }

    private void OnBuyButtonClicked()
    {
        _bottleManager.TryBuyBottle(_supplier);
    }
}
