using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeeRayGenerator : BasePeePart
{
    [SerializeField] private float spawnInterval;
    [SerializeField] private float rayDestroyDelay = 0.5f;

    [SerializeField] private Transform[] rayOrigins;

    [Space]
    [SerializeField] private PeeBox peeBoxPrefab;

    private float _lastSpawnTime;

    private void Update()
    {
        if(!IsActive) return;

        if (Time.time - _lastSpawnTime < spawnInterval) return;

        SpawnRay();
    }

    private void SpawnRay()
    {
        foreach(var rayOrigin in rayOrigins)
        {
            if (!rayOrigin.gameObject.activeInHierarchy) continue;

            var offset = new Vector3(0.0f, 0.0f, peeBoxPrefab.transform.localScale.z * 0.5f); ;
            PeeBox ray = Instantiate(peeBoxPrefab, rayOrigin.position + offset, rayOrigin.rotation);
            ray.SetDestroyTimer(rayDestroyDelay);
        }

        _lastSpawnTime = Time.time;
    }
}
