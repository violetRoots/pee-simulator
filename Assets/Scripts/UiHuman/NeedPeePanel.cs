using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NeedPeePanel : MonoBehaviour
{
    [SerializeField] private Gradient peeSliderGradient;

    [SerializeField] private Image peeSlider;

    public void UpdateSlider(float value)
    {
        peeSlider.transform.localScale = new Vector3(value, 1, 1);
        peeSlider.color = peeSliderGradient.Evaluate(value);
    }
}
