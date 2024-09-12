using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private float spawnPeeInterval = 0.1f;

    private TextMeshProUGUI _scoreTMP;

    private int _currentScore = 0;
    private float _lastSpawnPeeTime;

    private void Awake()
    {
        _scoreTMP = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (Time.time - _lastSpawnPeeTime >= spawnPeeInterval)
            {
                UpdateText();
            }
        }
    }

    private void UpdateText()
    {
        _currentScore += 10;
        _scoreTMP.text = $"YOUR SCORE: {_currentScore}";

        _lastSpawnPeeTime = Time.time;
    }
}
