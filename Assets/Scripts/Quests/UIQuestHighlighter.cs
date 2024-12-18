using System.Linq;
using UnityEngine;

public class UIQuestHighlighter : MonoBehaviour
{
    [SerializeField] private GameObject questHighlight;

    private QuestsManager _questsManager;

    private void Awake()
    {
        _questsManager = GameManager.Instance.QuestsManager;
    }

    private void Update()
    {
        UpdateQuestHighlight();
    }

    private void UpdateQuestHighlight()
    {
        var isAnyQuestFinished = _questsManager.GetQuests().Any(q => q.isFinished && !q.isOpened);
        questHighlight.SetActive(isAnyQuestFinished);
    }
}
