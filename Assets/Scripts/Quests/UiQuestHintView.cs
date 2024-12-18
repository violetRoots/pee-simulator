using Common.Localisation;
using TMPro;
using UnityEngine;

public class UiQuestHintView : MonoBehaviour
{
    [SerializeField] private TranslatedTextMeshPro title;
    [SerializeField] private TranslatedTextMeshPro description;
    [SerializeField] private TextMeshProUGUI progress;

    public void SetContext(QuestRuntimeInfo questInfo)
    {
        title.SetKey(questInfo.configData.title);
        description.SetKey(questInfo.configData.description, questInfo.configData.maxProgress);
        progress.text = $"{(int) questInfo.progressValue}/{questInfo.configData.maxProgress}";
    }
}
