using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using UnityEngine;

public class UiQuestPanel : MonoBehaviour
{
    [SerializeField] private UiQuestCell questCell;
    [SerializeField] private RectTransform questsContentObj;

    private QuestsManager _questsManager;

    private readonly List<UiQuestCell> _questCells = new();

    private void Awake()
    {
        _questsManager = GameManager.Instance.QuestsManager;

        _questsManager.onQuestProgressUpdated += UpdateQuestCells;

        InitQuests();
    }

    private void OnDestroy()
    {
        _questsManager.onQuestProgressUpdated -= UpdateQuestCells;
    }

    private void UpdateQuestCells(QuestConfig.QuestType questType)
    {
        var questCell = _questCells.Where(cell => cell.RuntimeInfo.configData.type == questType).FirstOrDefault();

        questCell?.UpdateProgressValue();

        if (questCell.RuntimeInfo.isFinished)
            Destroy(questCell.gameObject);
    }

    private void InitQuests()
    {
        var quests = _questsManager.GetUnfinishedQuests();
        foreach (var quest in quests)
        {
            var cell = Instantiate(questCell, questsContentObj);
            cell.SetContext(quest);

            _questCells.Add(cell);
        }
    }
}
