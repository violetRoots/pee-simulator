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

    private GameManager _gameManager;

    public void Init(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    public void TryBuyBottle(PeeSupplierConfig supplier)
    {
        if (_gameManager.Data.Money < supplier.price) return;

        _gameManager.Data.SetMoney(_gameManager.Data.Money - supplier.price);
        SpawnBottle(supplier);
    }

    public void SpawnBottle(PeeSupplierConfig supplier)
    {
        var pos = spawnOrigin.position + UnityEngine.Random.insideUnitSphere * spawnRadius;
        var bottle = GameObject.Instantiate(bottlePrefab, pos, Quaternion.identity, bottlesContainer);
        bottle.SetSupplier(supplier);
    }
}
