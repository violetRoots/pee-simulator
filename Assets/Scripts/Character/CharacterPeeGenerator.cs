using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPeeGenerator : BasePeePart
{
    [SerializeField] private float boilerTickValue = 0.0001f;
    [SerializeField] private float peeStrength = 1.0f;
    [SerializeField] private float spawnPeeInterval = 0.1f;

    [Space]
    [SerializeField] private PeeBox peeBoxPrefab;
    [SerializeField] private Transform[] peeOrigins;

    private QuestsManager _questManager;

    private float _lastSpawnPeeTime;

    private void Awake()
    {
        _questManager = GameManager.Instance.QuestsManager;
    }

    private void LateUpdate()
    {
        if (!IsActive) return;

        if (Time.time - _lastSpawnPeeTime >= spawnPeeInterval)
        {
            Pee();
        }
    }

    private void Pee()
    {
        _questManager.ChangeProgressQuest(QuestConfig.QuestType.AllYourself, 0.01f);

        foreach (var peeOrigin in peeOrigins)
        {
            if (!peeOrigin.gameObject.activeInHierarchy) continue;

            PeeBox peeBox = Instantiate(peeBoxPrefab, peeOrigin.position, Quaternion.LookRotation(transform.forward));
            peeBox.PeeForward(peeOrigin.forward, peeStrength);
        }

        _lastSpawnPeeTime = Time.time;
    }
}
