using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionControlsHelper : MonoBehaviour
{
    [SerializeField] private string key;

    [Space]
    [SerializeField] private BasicLookInteractionController interactionController;

    private UiGameplayManager _uiManager;

    private void Awake()
    {
        _uiManager = UiGameplayManager.Instance;
    }

    private void OnEnable()
    {
        interactionController.onSelect += ShowControls;
        interactionController.onDeselect += HideControls;
    }

    private void OnDisable()
    {
        interactionController.onSelect -= ShowControls;
        interactionController.onDeselect -= HideControls;
    }

    public void ShowControls()
    {
        _uiManager.ShowControls(key);
    }

    public void HideControls()
    {
        _uiManager.HideControls();
    }
}
