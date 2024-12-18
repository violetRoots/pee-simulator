using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats
{
    public bool firstLoad = true;

    public int money = 0;
    public int score = 0;
    public int daysCount = 0;

    public List<SupplierRuntimeInfo> runtimeSuppliers = new();
    public List<CheckRuntimeInfo> runtimeChecks = new();
    public List<QuestRuntimeInfo> runtimeQuests = new();
}


public static class PlayerStatsUtility
{
    public static void ChangeMoney(this PlayerStats playerStats, int money)
    {
        playerStats.money += money;

        GameManager.Instance.QuestsManager.ChangeProgressQuest(QuestConfig.QuestType.Earn, money);
    }

    public static void SetDays(this PlayerStats playerStats, int days)
    {
        playerStats.daysCount = days;
    }
}
