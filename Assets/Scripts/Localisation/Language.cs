using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "Language", menuName = "Configs/Language", order = 6)]
public class Language : ScriptableObject
{
    public string languageName;
    public TMP_FontAsset font;
}
