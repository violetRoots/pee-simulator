using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSound : MonoBehaviour
{
    [SerializeField] private SfxType type;
    [SerializeField] float volume = 0.1f;

    private void OnEnable()
    {
        gameObject.Play3DSound(type, volume);
    }

    private void OnDisable()
    {
        gameObject.Stop3DSound();
    }
}
