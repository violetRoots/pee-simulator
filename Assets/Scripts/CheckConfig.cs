using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Check", menuName = "Configs/CheckConfig", order = 3)]
public class CheckConfig : ScriptableObject
{
    public string title;
    [TextArea]
    public string description;
    public int term = 1;
    public int price = 1;
    public int surcharge = 1;
    public Color backgroundColor = Color.white;
}
