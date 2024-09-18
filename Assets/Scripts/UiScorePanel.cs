using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiScorePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    private GameManager _gameManager;

    private void Awake()
    {
        _gameManager = GameManager.Instance;
    }

    private void Update()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        scoreText.text = $"YOUR SCORE: {_gameManager.Data.Score}";
    }
}
