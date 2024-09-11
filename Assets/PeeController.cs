using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeeController : MonoBehaviour
{
    [SerializeField] private float peeStrength = 1.0f;
    [SerializeField] private float spawnPeeInterval = 0.1f;

    [Space]
    [SerializeField] private PeeBox peeBoxPrefab;
    private float _lastSpawnPeeTime;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if(Time.time - _lastSpawnPeeTime >= spawnPeeInterval)
            {
                Pee();
            }
        }
    }

    private void Pee()
    {
        var peeBox = Instantiate(peeBoxPrefab, transform.position, Quaternion.LookRotation(transform.forward));
        peeBox.PeeForward(transform.forward, peeStrength);

        _lastSpawnPeeTime = Time.time;
    }
}
