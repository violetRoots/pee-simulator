using Common.Localisation;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiSupplierCell : MonoBehaviour
{
    public SupplierRuntimeInfo RuntimeInfo { get; private set; }

    [SerializeField] private Image icon;
    [SerializeField] private TranslatedTextMeshPro title;
    [SerializeField] private TranslatedTextMeshPro description;
    [SerializeField] private TranslatedTextMeshPro characteristics;
    [SerializeField] private TranslatedTextMeshPro price;
    [SerializeField] private Button buyButton;

    [SerializeField] private string characteristicsPattern;
    [SerializeField] private string pricePattern;

    private BottleManager _bottleManager;

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

    public void SetContext(SupplierRuntimeInfo info)
    {
        RuntimeInfo = info;

        icon.sprite = RuntimeInfo.VisualInfo.icon;
        title.SetKey(RuntimeInfo.configData.title);
        description.SetKey(RuntimeInfo.configData.description);
        characteristics.SetKey(characteristicsPattern, RuntimeInfo.configData.satisfaction, RuntimeInfo.configData.causticity, RuntimeInfo.configData.saturation);
        price.SetKey(pricePattern, RuntimeInfo.configData.price);
    }

    private void OnBuyButtonClicked()
    {
        _bottleManager.TryBuyBottle(RuntimeInfo);

        AudioManager.StaticPlaySound(SfxType.ItemBought, 0.075f);
    }
}
