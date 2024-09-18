using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiMoneyPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;

    private GameManager _gameManager;

    private void Awake()
    {
        _gameManager = GameManager.Instance;
    }

    private void Update()
    {
        moneyText.text = $"{_gameManager.Data.Money}";
    }
}
