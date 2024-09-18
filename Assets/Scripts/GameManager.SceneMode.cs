using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public partial class GameManager
{
    public enum SceneState
    {
        Gameplay = 0,
        Ui = 1
    }

    public readonly ReactiveProperty<SceneState> sceneState = new ReactiveProperty<SceneState>(SceneState.Gameplay);

    public void SetSceneState(SceneState newMode)
    {
        sceneState.Value = newMode;
    }

    public bool IsGameplayInputEnabled()
    {
        return sceneState.Value == SceneState.Gameplay;
    }

    public bool IsUiInputEnabled()
    {
        return sceneState.Value == SceneState.Ui;
    }
}
