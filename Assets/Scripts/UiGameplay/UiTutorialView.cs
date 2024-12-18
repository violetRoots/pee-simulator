using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiTutorialView : MonoBehaviour
{
    [SerializeField] private Button exitButton;

    private GameManager _gameManager;

    private void Awake()
    {
        _gameManager = GameManager.Instance;

        exitButton.onClick.AddListener(OnExitButtonClicked);
    }

    private void OnEnable()
    {
        _gameManager.AddLock(this);
    }

    private void OnDisable()
    {
        _gameManager.RemoveLock(this);
    }

    private void OnDestroy()
    {
        exitButton.onClick.RemoveAllListeners();
    }

    private void OnExitButtonClicked()
    {
        gameObject.SetActive(false);
    }
}
