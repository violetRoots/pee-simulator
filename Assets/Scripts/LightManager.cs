using System;
using UnityEngine;

[Serializable]
public class LightManager
{
    [SerializeField] private Light directionalLight;

    public Light GetLight()
    {
        return directionalLight;
    }
}
