using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class PauseManager : SingletonMonoBehaviourBase<PauseManager>
{
    private GameManager _gameManager;

    private IDisposable _pauseSubscription;

    private void Awake()
    {

        _gameManager = GameManager.Instance;

        Play();
    }

    private void OnEnable()
    {
        _pauseSubscription = _gameManager.sceneState.Subscribe(OnSceneStateChanged);
    }

    private void OnDisable()
    {
        _pauseSubscription?.Dispose();
        _pauseSubscription = null;
    }

    private void OnSceneStateChanged(GameManager.SceneState newState)
    {
        if(newState == GameManager.SceneState.Ui)
        {
            Pause();
        }
        else
        {
            Play();
        }
    }

    private void Pause()
    {
        Time.timeScale = 0.0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Play()
    {
        Time.timeScale = 1.0f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
