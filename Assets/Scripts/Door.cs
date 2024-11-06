using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public enum DoorState
    {
        Closed = 0,
        Opened = 1,
    }

    public DoorState State => _state;

    public PlayerInteractionHandler InteractionHandler => interactionHandler;

    [SerializeField] private GameObject visual;
    [SerializeField] private Transform entrancePoint;

    [SerializeField] private PlayerInteractionHandler interactionHandler;

    private DoorState _state = DoorState.Closed;


    private void Awake()
    {
        interactionHandler.CustomInteration = OnInteract;
    }

    private void OnInteract()
    {
        if(_state == DoorState.Closed)
        {
            Open();
        }
        else if(_state == DoorState.Opened)
        {
            Close();
        }

        interactionHandler.SetInteractable(false);
    }

    private void Open()
    {
        _state = DoorState.Opened;

        visual.SetActive(false);
    }

    private void Close()
    {
        _state = DoorState.Closed;

        visual.SetActive(true);
    }

    public Transform GetEntrancePoint()
    {
        return entrancePoint;
    }
}
