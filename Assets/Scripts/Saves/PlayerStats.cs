using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats
{
    public int money = 0;
    public int score = 0;
    public int daysCount = 0;

    public List<ChecksManager.CheckRuntimeInfo> runtimeChecks = new();
    public List<QuestsManager.QuestRuntimeInfo> runtimeQuests = new();
}


public static class PlayerStatsUtility
{
    public static void ChangeMoney(this PlayerStats playerStats, int money)
    {
        playerStats.money += money;
    }

    public static void SetDays(this PlayerStats playerStats, int days)
    {
        playerStats.daysCount = days;
    }
}
