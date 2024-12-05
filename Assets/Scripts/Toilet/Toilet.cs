using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toilet : MonoBehaviour
{
    public BasicLookInteractionController InteractionHandler => lookInteractionController;

    [SerializeField] private BasicLookInteractionController lookInteractionController;

    private DayManager _dayManager;
    private UiGameplayManager _uiManager;

    private void Awake()
    {
        _dayManager = DayManager.Instance;
        _uiManager = UiGameplayManager.Instance;

        lookInteractionController.onInteract = OnInteract;
        lookInteractionController.onTalk = OnTalk;
    }

    private void OnInteract()
    {
        _uiManager.ShowShopView();
    }

    private void OnTalk()
    {
        if (_dayManager.state.Value == DayManager.DayState.NeedEndDay)
            _dayManager.state.Value = DayManager.DayState.NeedOpenDoors;
    }
}
