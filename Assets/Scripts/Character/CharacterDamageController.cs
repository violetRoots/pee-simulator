using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDamageController : MonoBehaviour
{
    public event Action<float> Damaged;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out CharacterDamageTrigger damageTrigger)) return;

        Damaged?.Invoke(Time.time);

        other.gameObject.SetActive(false);
    }
}
