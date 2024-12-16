using System;
using System.Linq;
using UnityEngine;

[Serializable]
public class KassaManager
{
    [SerializeField] private Kassa[] kassas;

    public Kassa GetKassaByDoor(Door door)
    {
        var kassasAttachedToDoor = kassas.Where(kassa => kassa.IsAttachedToDoor(door)).ToArray();
        return kassasAttachedToDoor[UnityEngine.Random.Range(0, kassasAttachedToDoor.Length)];
    }
}
