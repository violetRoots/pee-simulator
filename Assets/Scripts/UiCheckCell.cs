using Common.Localisation;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiCheckCell : MonoBehaviour
{
    public CheckConfig CheckConfig { get; private set; }

    [SerializeField] private TranslatedTextMeshPro title;
    [SerializeField] private TranslatedTextMeshPro description;
    [SerializeField] private TranslatedTextMeshPro term;
    [SerializeField] private TranslatedTextMeshPro price;

    [SerializeField] private Image background;

    [SerializeField] private string pricePattern;
    [SerializeField] private string termPattern;

    public void SetContext(CheckConfig check)
    {
        CheckConfig = check;

        title.SetKey(CheckConfig.title);
        description.SetKey(CheckConfig.description);
        term.SetKey(termPattern, CheckConfig.term);
        price.SetKey(pricePattern, CheckConfig.price);
        background.color = CheckConfig.backgroundColor;
    }
}
