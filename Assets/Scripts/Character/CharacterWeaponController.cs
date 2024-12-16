using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterWeaponController : MonoBehaviour
{
    [SerializeField] private Weapon[] weapons;

    public void ActivateWeaponFire()
    {
        foreach (var weapon in weapons)
        {
            weapon.Activate();
        }
    }

    public void DeactivateWeaponFire()
    {
        foreach (var weapon in weapons)
        {
            weapon.Deactivate();
        }
    }

    public void EnableWeapon(CircleItemConfig config)
    {
        var enableWeapons = weapons.Where(weapon => weapon.ItemConfig == config);

        foreach (var weapon in enableWeapons)
        {
            weapon.Enable();
        }
    }
}
