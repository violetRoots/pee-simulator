using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiCircleSegment : MonoBehaviour
{
    public CircleItemConfig ItemConfig => config;

    [SerializeField] private CircleItemConfig config;

    [Space]
    [SerializeField] private Color defaultColor;
    [SerializeField] private Color selectColor;

    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    public bool CanSelect()
    {
        return ItemConfig != null;
    }

    public void Select()
    {
        _image.color = selectColor;
    }

    public void Deselect()
    {
        _image.color = defaultColor;
    }
}
