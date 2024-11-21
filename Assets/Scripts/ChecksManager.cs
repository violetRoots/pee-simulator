using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

[Serializable]
public class ChecksManager
{
    public class CheckRuntimeInfo
    {
        public CheckConfig config;
        public int term;
    }

    [SerializeField] private CheckConfig[] checks;

    public readonly ReactiveCollection<CheckRuntimeInfo> runtimeChecks = new ReactiveCollection<CheckRuntimeInfo>();

    private DayManager _dayManager;
    private GameplayDataContainer _data;

    public void Init()
    {
        _dayManager = DayManager.Instance;
        _data = GameManager.Instance.Data;



        _dayManager.onPastDay += UpdateCheckTerms;
        _dayManager.onPastDay += AddRandomCheck;
    }

    public void Dispose()
    {
        _dayManager.onPastDay -= UpdateCheckTerms;
        _dayManager.onPastDay -= AddRandomCheck;
    }

    public void AddRandomCheck()
    {
        var check = checks[UnityEngine.Random.Range(0, checks.Length)];

        var checkInfo = new CheckRuntimeInfo()
        {
            config = check,
            term = check.term,
        };

        runtimeChecks.Add(checkInfo);
    }

    private void UpdateCheckTerms()
    {
        foreach (var checkInfo in runtimeChecks)
        {
            checkInfo.term--;

            if(checkInfo.term < 0)
            {
                _data.ChangeMoney(-(checkInfo.config.price + checkInfo.config.surcharge));
            }
        }
    }
}
