using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSetup : MonoBehaviour
{
    private void Awake()
    {
        LanguageManager.Instance.SetEnglishLanguage();
    }
}
