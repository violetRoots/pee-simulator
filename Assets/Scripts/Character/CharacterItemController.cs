using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterItemController : MonoBehaviour
{
    [SerializeField] private float throwForce = 100.0f;

    [Space]
    [SerializeField] private string controlsHintKey;

    [SerializeField] private Transform itemContainer;
    [SerializeField] private Transform itemThrowContainer;

    [HideInInspector]
    [SerializeField] private CharacterProvider _characterProvider;

    private CharacterInteractionController _interactionController;

    private InputManager _inputManager;
    private UiGameplayManager _uiGameplayManager;

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
        _uiGameplayManager = UiGameplayManager.Instance;
        _interactionController = _characterProvider.InteractionController;
    }

    private void OnEnable()
    {
        _inputManager.OnLeftMouseDown += ThrowItem;
        _inputManager.OnItemDropButtonDown += DropItem;
    }

    private void OnDisable()
    {
        _inputManager.OnLeftMouseDown -= ThrowItem;
        _inputManager.OnItemDropButtonDown -= DropItem;
    }

    public void GetItem(Item item)
    {
        if (_currentItem != null) return;

        _interactionController.SetInteractionMode(CharacterInteractionController.CharacterInteractionMode.CarryItem);

        _currentItem = item;

        _currentItem.Attach(itemContainer);

        _uiGameplayManager.ShowControls(controlsHintKey, true);
    }

    public Item PopItem()
    {
        if (!HasItem()) return null;

        var item = _currentItem;
        _currentItem = null;

        item.Detach();

        _interactionController.SetInteractionMode(CharacterInteractionController.CharacterInteractionMode.Gameplay);

        _uiGameplayManager.HideControls(true);

        return item;
    }

    public void DropItem()
    {
        if(!HasItem()) return;

        var item = PopItem();
        item.gameObject.Play3DSound(SfxType.BottleFail);
    }

    public void ThrowItem()
    {
        if (!HasItem()) return;

        _characterProvider.transform.SetParent(itemThrowContainer, false);

        var item = PopItem();

        item.Throw(itemThrowContainer.forward, throwForce);
        item.gameObject.Play3DSound(SfxType.BottleFail);
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
