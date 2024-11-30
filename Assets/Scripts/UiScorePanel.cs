using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiScorePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    private PlayerStats _playerStats;

    private void Awake()
    {
        _playerStats = SavesManager.Instance.PlayerStats.Value;
    }

    private void Update()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        scoreText.text = $"YOUR SCORE: {_playerStats.score}";
    }
}
