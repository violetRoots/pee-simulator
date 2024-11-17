using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ChecksManager
{
    public class CheckRuntimeInfo
    {
        public CheckConfig config;
    }

    [SerializeField] private CheckConfig[] checks;

    private readonly List<CheckRuntimeInfo> checksInfo = new List<CheckRuntimeInfo>();

    public void Init()
    {
        foreach (var check in checks)
        {
            var checkInfo = new CheckRuntimeInfo()
            {
                config = check
            };

            checksInfo.Add(checkInfo);
        }
    }

    public CheckRuntimeInfo[] GetChecks()
    {
        return checksInfo.ToArray();
    }
}
