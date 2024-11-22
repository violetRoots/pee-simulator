using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Common.Localisation;

public class UiQuestCell : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TranslatedTextMeshPro title;
    [SerializeField] private TranslatedTextMeshPro description;
    [SerializeField] private TextMeshProUGUI progress;

    [TextArea]
    [SerializeField] private string progressPattern;

    public void SetContext(QuestConfig quest)
    {
        icon.sprite = quest.supplier.iconSprite;
        title.SetKey(quest.title);
        description.SetKey(quest.description, quest.progress);
        progress.text = string.Format(progressPattern, quest.progress);
    }
}
