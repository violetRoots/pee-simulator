using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiTimePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeTMP;

    private DayManager _dayManager;

    DateTime _begin = new DateTime(2024, 1, 1, 06, 00, 00);
    DateTime _end = new DateTime(2024, 1, 1, 23, 00, 00);

    private void Awake()
    {
        _dayManager = DayManager.Instance;
    }

    private void Update()
    {
        UpdateTimeText();
    }

    private void UpdateTimeText()
    {
        var allSpan = _end - _begin;
        var currentTicks = (long)Mathf.Lerp(0.0f, allSpan.Ticks, _dayManager.DayProgressValue);
        var currentSpan = new TimeSpan(_begin.Ticks + currentTicks);

        timeTMP.text = currentSpan.ToString("hh\\:mm");
    }
}
