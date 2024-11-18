using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toilet : MonoBehaviour
{
    public BasicLookInteractionController InteractionHandler => lookInteractionController;

    [SerializeField] private BasicLookInteractionController lookInteractionController;

    private UiManager _uiManager;

    private void Awake()
    {
        _uiManager = UiManager.Instance;

        lookInteractionController.CustomInteration = OnInteract;
    }

    private void OnInteract()
    {
        //if(_dayManager.state.Value == DayManager.DayState.NeedEndDay)
        //    _dayManager.state.Value = DayManager.DayState.NeedOpenDoors;

        _uiManager.ShowShopView();
    }
}
