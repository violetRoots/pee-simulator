using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSetup : MonoBehaviour
{
    private void Awake()
    {
        if(!LanguageManager.IsInited)
            LanguageManager.Instance.SetEnglishLanguage();
    }
}
