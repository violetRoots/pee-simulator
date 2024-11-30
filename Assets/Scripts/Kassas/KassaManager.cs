using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class KassaManager
{
    [SerializeField] private Kassa[] kassas;

    public Kassa GetRandomKassa()
    {
        var index = UnityEngine.Random.Range(0, kassas.Length); 
        return kassas[index];
    }
}
