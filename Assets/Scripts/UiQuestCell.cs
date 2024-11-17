using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiQuestCell : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI progress;

    [TextArea]
    [SerializeField] private string progressPattern;

    public void SetContext(QuestConfig quest)
    {
        icon.sprite = quest.supplier.iconSprite;
        title.text = quest.title;
        description.text = quest.description;
        progress.text = string.Format(progressPattern, progress);
    }
}
