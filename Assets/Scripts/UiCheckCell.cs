using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiCheckCell : MonoBehaviour
{
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
        title.text = check.title;
        description.text = check.description;
        term.text = string.Format(termPattern, check.term);
        price.text = string.Format(pricePattern, check.price);
        background.color = check.backgroundColor;
    }
}
