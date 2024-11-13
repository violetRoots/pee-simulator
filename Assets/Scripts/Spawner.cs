using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [MinMaxSlider(0, 100)]
    [SerializeField] private Vector2 spawnTimeBounds;
    [SerializeField] private AnimationCurve zombieCurve;
    [SerializeField] private bool spawnOnStart = true;

    [SerializeField] private Door[] doors;
    [SerializeField] private Transform npcContainer;

    [SerializeField] private GameObject[] humans;
    [SerializeField] private GameObject zombie;

    private float _normalizedDayValue;

    private bool _canSpawn = true;

    private void Start()
    {
        if(spawnOnStart)
            StartSpawn();
    }

    public void StartSpawn()
    {
        _canSpawn = true;
        StartCoroutine(SpawnProcess());
    }

    public void StopSpawn()
    {
        _canSpawn = false;
        StopCoroutine(SpawnProcess());
    }

    public void SetDayValue(float normalizedValue)
    {
        _normalizedDayValue = normalizedValue;
    }

    private IEnumerator SpawnProcess()
    {
        while (_canSpawn)
        {
            var openedDoors = doors.Where(door => door.State == Door.DoorState.Opened).ToArray();

            if(openedDoors.Length > 0)
            {
                var spawnPoints = openedDoors.Select(door => door.GetEntrancePoint()).ToArray();
                var spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

                var zombieChance = zombieCurve.Evaluate(_normalizedDayValue);
                var spawnObj = Random.value <= zombieChance ? zombie : humans[Random.Range(0, humans.Length)];

                NavMeshUtility.GetNavMeshPoint(spawnPoint.position, out Vector3 spawnPosition);
                Instantiate(spawnObj, spawnPosition, spawnPoint.rotation, npcContainer);
            }

            var randTime = Random.Range(spawnTimeBounds.x, spawnTimeBounds.y);
            yield return new WaitForSeconds(randTime);
        }
    }
}
