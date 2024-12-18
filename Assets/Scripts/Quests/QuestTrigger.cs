using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrigger : MonoBehaviour
{
    [SerializeField] private QuestConfig.QuestType type;
    [SerializeField] private float amount = 1.0f;

    private QuestsManager _questsManager;

    private void Awake()
    {
        _questsManager = GameManager.Instance.QuestsManager;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out CharacterProvider _)) return;

        _questsManager.ChangeProgressQuest(type, amount);

        Debug.Log($"Quest tirggered: {type} by {amount}");
    }
}
