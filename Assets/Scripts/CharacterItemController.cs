using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterItemController : MonoBehaviour
{
    [SerializeField] private Transform itemContainer;

    [HideInInspector]
    [SerializeField] private CharacterProvider _characterProvider;
    private CharacterInteractionController _interactionController;

    private InputManager _inputManager;

#if UNITY_EDITOR
    private void OnValidate()
    {
        _characterProvider = GetComponent<CharacterProvider>();

    }
#endif

    private Item _currentItem;

    private void Awake()
    {
        _inputManager = InputManager.Instance;
        _interactionController = _characterProvider.InteractionController;
    }

    private void OnEnable()
    {
        _inputManager.OnItemDropButtonDown += DropItem;
    }

    private void OnDisable()
    {
        _inputManager.OnItemDropButtonDown -= DropItem;
    }

    public void GetItem(Item item)
    {
        if (_currentItem != null) return;

        _interactionController.SetInteractionMode(CharacterInteractionController.CharacterInteractionMode.CarryItem);

        _currentItem = item;

        _currentItem.Attach(itemContainer);
    }

    public void PopItem()
    {
        if (_currentItem == null) return;

        _currentItem.Detach();
        _currentItem = null;

        _interactionController.SetInteractionMode(CharacterInteractionController.CharacterInteractionMode.Gameplay);
    }

    public void DropItem()
    {
        PopItem();
    }

    public bool HasItem()
    {
        return _currentItem != null;
    }

    public bool TryGetSpecificItem<T>(out T item) where T : Item
    {
        item = _currentItem as T;
        return HasItem() && item != null;
    }
}
