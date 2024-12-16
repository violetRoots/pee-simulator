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

    [SerializeField] private Transform npcContainer;

    [SerializeField] private HumanProvider human;
    [SerializeField] private ZombieProvider zombie;

    private DoorsManager _doorsManager;
    private KassaManager _kassaManager;

    private float _normalizedDayValue;

    private bool _canSpawn = true;

    private void Awake()
    {
        _doorsManager = GameManager.Instance.DoorsManager;
        _kassaManager = GameManager.Instance.KassaManager;
    }

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
            var openedDoors = _doorsManager.Doors.Where(door => door.State == Door.DoorState.Opened).ToArray();

            if (openedDoors.Length > 0)
            {
                var door = openedDoors[Random.Range(0, openedDoors.Length)];
                var kassa = _kassaManager.GetKassaByDoor(door);
                var spawnPoint = door.GetEntrancePoint();

                var zombieChance = zombieCurve.Evaluate(_normalizedDayValue);
                if(Random.value <= zombieChance)
                {
                    NavMeshUtility.GetNavMeshPoint(spawnPoint.position, out Vector3 zombieSpawnPos);
                    Instantiate(zombie, zombieSpawnPos, spawnPoint.rotation, npcContainer);
                }
                else
                {
                    NavMeshUtility.GetNavMeshPoint(spawnPoint.position, out Vector3 HumanSpawnPos);
                    HumanProvider newHuman = Instantiate(human, HumanSpawnPos, spawnPoint.rotation, npcContainer);

                    newHuman.StateController.SetBehaviourData(door, kassa);
                }
            }

            var randTime = Random.Range(spawnTimeBounds.x, spawnTimeBounds.y);
            yield return new WaitForSeconds(randTime);
        }
    }
}
