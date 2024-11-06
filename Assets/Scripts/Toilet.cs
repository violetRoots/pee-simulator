using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toilet : MonoBehaviour
{
    public PlayerInteractionHandler InteractionHandler => interactionHandler;

    [SerializeField] private PlayerInteractionHandler interactionHandler;

    private DayManager _dayManager;

    private void Awake()
    {
        _dayManager = DayManager.Instance;

        interactionHandler.CustomInteration = OnInteract;
        interactionHandler.SetInteractable(false);
    }

    private void OnInteract()
    {
        _dayManager.state.Value = DayManager.DayState.NeedOpenDoors;

        interactionHandler.SetInteractable(false);
    }
}
