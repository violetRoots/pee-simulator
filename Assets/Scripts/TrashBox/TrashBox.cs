using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBox : MonoBehaviour
{
    [SerializeField] private BasicLookInteractionController lookInteractionController;

    private CharacterItemController _characterItemController;

    private void Awake()
    {
        _characterItemController = GameManager.Instance.CharacterProvider.ItemController;

        lookInteractionController.onInteract = OnInteract;
    }

    private void OnInteract()
    {
        if (_characterItemController.TryGetSpecificItem(out Bottle bottle))
        {
            _characterItemController.PopItem();
            Destroy(bottle.gameObject);
        }
    }
}
