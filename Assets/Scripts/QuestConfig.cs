using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "Configs/QuestConfig", order = 2)]
public class QuestConfig : ScriptableObject
{
    public PeeSupplierConfig supplier;
    public string title;
    [TextArea]
    public string description;
    public int progress = 1;
}
