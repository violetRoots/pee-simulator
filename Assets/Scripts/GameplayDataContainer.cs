using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayDataContainer
{
    public int Money { get; private set; }

    public void SetMoney(int money)
    {
        Money = money;
    }

    public int Score { get; private set; }

    public void SetScore(int score)
    {
        Score = score;
    }
}
