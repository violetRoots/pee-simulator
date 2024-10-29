using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [MinMaxSlider(0, 100)]
    [SerializeField] private Vector2 spawnTimeBounds;
    [Range(0, 1)]
    [SerializeField] private float zombieSpawnChance = 0.1f;
    [SerializeField] private bool spawnOnStart = true;

    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Transform npcContainer;

    [SerializeField] private GameObject[] humans;
    [SerializeField] private GameObject zombie;

    private bool _canSpawn = true;

    private void Start()
    {
        if(spawnOnStart)
            StartCoroutine(SpawnProcess());
    }

    private IEnumerator SpawnProcess()
    {
        while (_canSpawn)
        {
            var spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            var spawnObj = Random.value <= zombieSpawnChance ? zombie : humans[Random.Range(0, humans.Length)];
            Instantiate(spawnObj, spawnPoint.position, spawnPoint.rotation, npcContainer);

            yield return new WaitForSeconds(Random.Range(spawnTimeBounds.x, spawnTimeBounds.y));
        }
    }
}
