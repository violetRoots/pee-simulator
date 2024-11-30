using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Common.Localisation;

public class UiQuestCell : MonoBehaviour
{
    public QuestsManager.QuestRuntimeInfo RuntimeInfo { get; private set; }

    [SerializeField] private Image icon;
    [SerializeField] private TranslatedTextMeshPro title;
    [SerializeField] private TranslatedTextMeshPro description;
    [SerializeField] private TextMeshProUGUI progress;
    [SerializeField] private Image progressFiller;

    [TextArea]
    [SerializeField] private string progressPattern;

    public void SetContext(QuestsManager.QuestRuntimeInfo questnfo)
    {
        RuntimeInfo = questnfo;

        var spriteId = RuntimeInfo.configData.supplierData.iconSpriteId;
        icon.sprite = DatabaseManager.Instance.GetSprite(spriteId);
        title.SetKey(RuntimeInfo.configData.title);
        description.SetKey(RuntimeInfo.configData.description, RuntimeInfo.configData.maxProgress);

        UpdateProgressValue();
    }

    public void UpdateProgressValue()
    {
        progressFiller.fillAmount = RuntimeInfo.progressValue / RuntimeInfo.configData.maxProgress;
        progress.text = string.Format(progressPattern, (int) RuntimeInfo.progressValue, RuntimeInfo.configData.maxProgress);
    }
}
