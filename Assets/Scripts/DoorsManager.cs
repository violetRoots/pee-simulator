using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class DoorsManager
{
    private Door[] _doors;

    public void Init()
    {
        _doors = GameObject.FindObjectsOfType<Door>();
    }

    public Door GetRandomDoor()
    {
        return _doors[UnityEngine.Random.Range(0, _doors.Length)];
    }

    public void SetDoorsInteractable(bool isInteractable)
    {
        foreach (Door door in _doors)
        {
            door.InteractionHandler.SetInteractable(isInteractable);
        }
    }

    public bool IsAllDoorHasState(Door.DoorState state)
    {
        return _doors.All(door =>  door.State == state);
    }

    public bool IsAnyDoorHasState(Door.DoorState state)
    {
        return _doors.Any(door => door.State == state);
    }
}
