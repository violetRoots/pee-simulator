using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class BottleManager
{
    [SerializeField] private float spawnRadius = 1.0f;

    [SerializeField] private Transform spawnOrigin;
    [SerializeField] private Transform bottlesContainer;

    [SerializeField] private Bottle bottlePrefab;

    private PlayerStats _playerStats;

    public void Init()
    {
        _playerStats = SavesManager.Instance.PlayerStats.Value;
    }

    public void TryBuyBottle(SuppliersManager.SupplierRuntimeInfo info)
    {
        if (_playerStats.money < info.configData.price) return;

        _playerStats.ChangeMoney(-info.configData.price);
        SpawnBottle(info);
    }

    public void SpawnBottle(SuppliersManager.SupplierRuntimeInfo info)
    {
        var pos = spawnOrigin.position + UnityEngine.Random.insideUnitSphere * spawnRadius;
        var bottle = GameObject.Instantiate(bottlePrefab, pos, Quaternion.identity, bottlesContainer);
        bottle.SetContext(info);
    }
}
