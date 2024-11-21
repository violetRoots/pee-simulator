using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadManager : SingletonFromResourcesBase<LoadManager>
{
    [SerializeField] private string mainMenuSceneName;
    [SerializeField] private string gameplaySceneName;

    public void LoadGameplayScene()
    {
        LoadScene(gameplaySceneName);
    }

    public void LoadMainMenuScene()
    {
        LoadScene(mainMenuSceneName);
    }

    private void LoadScene(string sceneName)
    {
        Debug.Log("Load: " + sceneName);

        SceneManager.LoadScene(sceneName);
    }
}
