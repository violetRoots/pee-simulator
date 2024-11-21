using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiCheckCell : MonoBehaviour
{
    public CheckConfig CheckConfig { get; private set; }

    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI term;
    [SerializeField] private TextMeshProUGUI price;

    [SerializeField] private Image background;

    [TextArea]
    [SerializeField] private string pricePattern;
    [TextArea]
    [SerializeField] private string termPattern;

    public void SetContext(CheckConfig check)
    {
        CheckConfig = check;

        title.text = CheckConfig.title;
        description.text = CheckConfig.description;
        term.text = string.Format(termPattern, CheckConfig.term);
        price.text = string.Format(pricePattern, CheckConfig.price);
        background.color = CheckConfig.backgroundColor;
    }
}
