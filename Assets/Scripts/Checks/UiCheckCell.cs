using Common.Localisation;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiCheckCell : MonoBehaviour
{
    public ChecksManager.CheckRuntimeInfo RuntimeInfo { get; private set; }

    [SerializeField] private TranslatedTextMeshPro title;
    [SerializeField] private TranslatedTextMeshPro description;
    [SerializeField] private TranslatedTextMeshPro term;
    [SerializeField] private TranslatedTextMeshPro price;

    [SerializeField] private Button payButton;

    [SerializeField] private Image background;

    [SerializeField] private string pricePattern;
    [SerializeField] private string termPattern;

    public void SetContext(ChecksManager.CheckRuntimeInfo checkInfo, Action payButtonAction)
    {
        RuntimeInfo = checkInfo;

        title.SetKey(RuntimeInfo.configData.title);
        description.SetKey(RuntimeInfo.configData.description);
        term.SetKey(termPattern, RuntimeInfo.configData.term);
        price.SetKey(pricePattern, RuntimeInfo.configData.price);
        background.color = RuntimeInfo.configData.backgroundColor;

        payButton.onClick.AddListener(() => payButtonAction?.Invoke());
    }
}
