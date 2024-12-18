using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using UniRx;
using UnityEngine;

[Serializable]
public class ChecksManager
{
    [SerializeField] private CheckConfig[] checkConfigs;

    public ReactiveCollection<CheckRuntimeInfo> runtimeChecks;

    private DayManager _dayManager;
    private PlayerStats _playerStats;

    public void Init()
    {
        _dayManager = DayManager.Instance;
        _playerStats = SavesManager.Instance.PlayerStats.Value;

        runtimeChecks = new ReactiveCollection<CheckRuntimeInfo>(_playerStats.runtimeChecks);

        _dayManager.onPastDay += OnPastDay;
    }

    public void Dispose()
    {
        _dayManager.onPastDay -= OnPastDay;
    }

    private void OnPastDay(int daysCount)
    {
        AddRandomCheck();
        UpdateCheckTerms();

        _playerStats.runtimeChecks = runtimeChecks.ToList();
    }

    private void AddRandomCheck()
    {
        var config = checkConfigs[UnityEngine.Random.Range(0, checkConfigs.Length)];

        var checkInfo = new CheckRuntimeInfo()
        {
            configData = config.data,
            term = config.data.term,
        };

        runtimeChecks.Add(checkInfo);
    }

    public void PayCheck(CheckRuntimeInfo info)
    {
        var checkInfo = runtimeChecks.Where(i => i == info).FirstOrDefault();

        if (checkInfo == null) return;

        runtimeChecks.Remove(checkInfo);
        _playerStats.ChangeMoney(-info.configData.price);
    }

    private void UpdateCheckTerms()
    {
        var removeCheck = new List<CheckRuntimeInfo>();

        foreach (var checkInfo in runtimeChecks)
        {
            checkInfo.term--;

            if (checkInfo.term >= 0) continue;

            removeCheck.Add(checkInfo);
        }

        foreach (var checkInfo in removeCheck)
        {
            runtimeChecks.Remove(checkInfo);

            _playerStats.ChangeMoney(-(checkInfo.configData.price + checkInfo.configData.surcharge));
        }
    }
}

[Serializable]
public class CheckRuntimeInfo
{
    public CheckConfig.CheckConfigData configData;
    public int term;
}
