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

    public BasicLookInteractionController InteractionHandler => interactionHandler;

    [SerializeField] private GameObject visual;
    [SerializeField] private Transform entrancePoint;

    [SerializeField] private BasicLookInteractionController interactionHandler;
    [SerializeField] private DayStateMarker openDoorMarker;
    [SerializeField] private DayStateMarker closeDoorMarker;

    private QuestsManager _questsManager;

    private DoorState _state = DoorState.Closed;


    private void Awake()
    {
        _questsManager = GameManager.Instance.QuestsManager;

        interactionHandler.onInteract = OnInteract;
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
        openDoorMarker.SetIsActive(false);
        closeDoorMarker.SetIsActive(false);
    }

    private void Open()
    {
        _state = DoorState.Opened;

        visual.SetActive(false);

        _questsManager.ChangeProgressQuest(QuestConfig.QuestType.OpenDoors, 1);
        gameObject.Play3DSound(SfxType.DoorOpened, 1);
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
