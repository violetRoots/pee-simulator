using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterWeaponController : MonoBehaviour
{
    [SerializeField] private Weapon[] weapons;

    private PlayerStats _playerStats;
    private UiGameplayManager _uiGameplayManager;

    private void Awake()
    {
        _playerStats = SavesManager.Instance.PlayerStats.Value;
        _uiGameplayManager = UiGameplayManager.Instance;
    }

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
        if (_playerStats.money < config.price) return;

        var enableWeapons = weapons.Where(weapon => weapon.ItemConfig == config);

        foreach (var weapon in enableWeapons)
        {
            weapon.Enable();
        }

        _playerStats.ChangeMoney(-config.price);

        _uiGameplayManager.SetCircleItemUsed(config);
    }
}
