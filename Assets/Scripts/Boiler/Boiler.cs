using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boiler : MonoBehaviour
{
    public Bottle AttachedBottle => _attachedBottle;

    [SerializeField] private Transform bottleOrigin;

    [SerializeField] private BasicLookInteractionController lookInteractionController;

    private CharacterItemController _characterItemController;

    private Bottle _attachedBottle;

    private void Awake()
    {
        _characterItemController = GameManager.Instance.CharacterProvider.ItemController;

        lookInteractionController.onInteract = OnInteract;
    }

    private void OnInteract()
    {
        if (_characterItemController.TryGetSpecificItem(out Bottle bottle) && _attachedBottle == null)
        {
            _characterItemController.PopItem();
            bottle.Attach(bottleOrigin);
            _attachedBottle = bottle;

            gameObject.Play3DSound(SfxType.BottleLoad);
        }
        else if (!_characterItemController.HasItem() && _attachedBottle != null)
        {
            _characterItemController.GetItem(_attachedBottle);
            _attachedBottle = null;
        }
    }
}
