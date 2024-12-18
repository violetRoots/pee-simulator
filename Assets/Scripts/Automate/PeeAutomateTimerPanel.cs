using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PeeAutomateTimerPanel : MonoBehaviour
{
    [SerializeField] private Gradient timerGradient;

    [SerializeField] private Image timerImage;
    [SerializeField] private GameObject bottleHint;

    public void UpdateValue(float value)
    {
        timerImage.fillAmount = value;
        timerImage.color = timerGradient.Evaluate(value);
    }

    public void UpdateHint(bool isVisible)
    {
        bottleHint.SetActive(isVisible);
    }
}
