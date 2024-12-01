using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBackgroundColorController : MonoBehaviour
{
    [SerializeField] private float hueChange = 1.0f;

    private Image background;

    private void Awake()
    {
        background = GetComponent<Image>();
    }

    private void Update()
    {
        Vector3 newColor;
        Color.RGBToHSV(background.color, out newColor.x, out newColor.y, out newColor.z);
        newColor.x = Mathf.Repeat(newColor.x + hueChange * Time.deltaTime, 360);
        background.color = Color.HSVToRGB(newColor.x, newColor.y, newColor.z);
    }
}
