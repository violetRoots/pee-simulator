using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayDataContainer
{
    private SaveGameManager _saveGameManager;
    private DayManager _dayManager;

    public void Init()
    {
        _saveGameManager = SaveGameManager.Instance;
        _dayManager = DayManager.Instance;

        DaysCount = _saveGameManager.DaysCount;
        Money = _saveGameManager.Money;

        _dayManager.onPastDay += OnPastDayHandler;
    }

    public void Dispose()
    {
        _dayManager.onPastDay -= OnPastDayHandler;
    }

    private void OnPastDayHandler()
    {
        DaysCount++;
        Debug.Log("DAYS: " + DaysCount);

        SaveData();
    }

    public void SaveData()
    {
        _saveGameManager.DaysCount = DaysCount;
        _saveGameManager.Money = Money;
    }

    public int DaysCount { get; private set; }

    public int Money
    {
        get => _money;
        private set => _money = (int) Mathf.Clamp(value, 0, Mathf.Infinity);
    }
    private int _money;

    public void SetMoney(int newMoney)
    {
        Money = newMoney;
    }

    public void ChangeMoney(int money)
    {
        Money += money;
    }

    public int Score { get; private set; }
    public void SetScore(int score)
    {
        Score = score;
    }
}
