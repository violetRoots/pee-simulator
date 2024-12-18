using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBox : MonoBehaviour
{
    [SerializeField] private BasicLookInteractionController lookInteractionController;

    private QuestsManager _questsManager;
    private CharacterItemController _characterItemController;

    private void Awake()
    {
        _questsManager = GameManager.Instance.QuestsManager;

        _characterItemController = GameManager.Instance.CharacterProvider.ItemController;

        lookInteractionController.onInteract = OnInteract;
    }

    private void OnInteract()
    {
        if (_characterItemController.TryGetSpecificItem(out Bottle bottle))
        {
            if (bottle.FillAmount >= 0.9f)
                _questsManager.ChangeProgressQuest(QuestConfig.QuestType.FullBottle, 1);

            _characterItemController.PopItem();
            Destroy(bottle.gameObject);
        }
    }
}
