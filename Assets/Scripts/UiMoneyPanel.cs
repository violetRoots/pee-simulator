using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiMoneyPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;

    private PlayerStats _playerStats;

    private void Awake()
    {
        _playerStats = SavesManager.Instance.PlayerStats.Value;
    }

    private void Update()
    {
        moneyText.text = $"{_playerStats.money}";
    }
}
