using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using UnityEngine;

public class UiQuestPanel : MonoBehaviour
{
    [SerializeField] private UiQuestCell questCell;
    [SerializeField] private RectTransform questsContentObj;

    private SuppliersManager _suppliersManager;
    private QuestsManager _questsManager;

    private readonly List<UiQuestCell> _questCells = new();

    private void Awake()
    {
        _suppliersManager = GameManager.Instance.SuppliersManager;
        _questsManager = GameManager.Instance.QuestsManager;

        _questsManager.onQuestProgressUpdated += UpdateQuestCells;

        InitQuests();
    }

    private void OnDestroy()
    {
        _questsManager.onQuestProgressUpdated -= UpdateQuestCells;
    }

    private void UpdateQuestCells(QuestRuntimeInfo questInfo)
    {
        var questCell = _questCells.Where(cell => cell.RuntimeInfo == questInfo).FirstOrDefault();

        questCell?.UpdateProgressValue();
    }

    private void InitQuests()
    {
        var quests = _questsManager.GetQuests();
        foreach (var quest in quests)
        {
            var cell = Instantiate(questCell, questsContentObj);
            cell.SetContext(quest, () =>
            {
                _suppliersManager.SetAvailableByQuest(quest);
            });

            _questCells.Add(cell);
        }
    }
}
