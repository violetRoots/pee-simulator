using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UiQuestHintPanel : MonoBehaviour
{
    [SerializeField] private UiQuestHintView[] hintViews;

    private QuestsManager _questsManager;

    private void Awake()
    {
        _questsManager = GameManager.Instance.QuestsManager;

        UpdateHintViews();
    }

    private void OnEnable()
    {
        _questsManager.onQuestProgressUpdated += OnQuestProgressUpdated;
    }

    private void OnDisable()
    {
        _questsManager.onQuestProgressUpdated -= OnQuestProgressUpdated;
    }

    private void OnQuestProgressUpdated(QuestRuntimeInfo updatedQuest)
    {
        UpdateHintViews();
    }

    private void UpdateHintViews()
    {
        var questsInfo = _questsManager.GetQuests()
                                       .Where(q => !q.isFinished)
                                       .OrderBy(q => q.configData.hintPriority)
                                       .ToArray();


        for(var i = 0; i < hintViews.Length; i++)
        {
            var hintView = hintViews[i];

            if(i >= questsInfo.Length)
            {
                hintView.gameObject.SetActive(false);
                break;
            }
                
            hintView.gameObject.SetActive(true);
            hintView.SetContext(questsInfo[i]);
        }
    }
}
