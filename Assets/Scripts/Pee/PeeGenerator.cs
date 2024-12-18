using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeeGenerator : BasePeePart
{
    [SerializeField] private float peeStrength = 1.0f;
    [SerializeField] private float spawnPeeInterval = 0.1f;

    [Space]
    [SerializeField] private PeeBox peeBoxPrefab;
    [SerializeField] private Transform[] peeOrigins;

    private BoilerSystem _boilerSystem;

    private float _lastSpawnPeeTime;

    private void Update()
    {
        if (!IsActive) return;

        if (Time.time - _lastSpawnPeeTime >= spawnPeeInterval)
        {
            Pee();
        }
    }

    public void Init(BoilerSystem boilerSystem)
    {
        _boilerSystem = boilerSystem;
    }

    private void Pee()
    {
        foreach (var peeOrigin in peeOrigins)
        {
            PeeBox peeBox = Instantiate(peeBoxPrefab, peeOrigin.position, Quaternion.LookRotation(transform.forward));
            peeBox.PeeForward(peeOrigin.forward, peeStrength);

            _boilerSystem.TickPee();
        }

        _lastSpawnPeeTime = Time.time;
    }

    protected override void OnActivated()
    {
        base.OnActivated();
        var type = Random.value < 0.5f ? SfxType.PissLoop1 : SfxType.PissLoop2;
        gameObject.Play3DSound(type, 3.0f, true);
    }

    protected override void OnDeactivated()
    {
        base.OnDeactivated();
        gameObject.Stop3DSound();
    }
}
