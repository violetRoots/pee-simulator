using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLookInteractionController : MonoBehaviour
{
    [SerializeField] private float castRadius = 0.01f;
    [SerializeField] private float castDistance = 5.0f;

    [SerializeField] private Transform raycastOrigin;

    private GameManager _gameManager;
    private InputManager _inputManager;

    private BasicLookInteractionController _selectedLookInteraction;


    private void Awake()
    {
        _gameManager = GameManager.Instance;
        _inputManager = InputManager.Instance;
    }

    private void OnEnable()
    {
        _inputManager.OnInterationButtonDown += OnInteract;
        _inputManager.OnTalkButtonDown += OnTalk;
    }

    private void OnDisable()
    {
        _inputManager.OnInterationButtonDown -= OnInteract;
        _inputManager.OnTalkButtonDown += OnTalk;
    }

    private void Update()
    {
        if (Physics.SphereCast(raycastOrigin.position, castRadius, raycastOrigin.forward, out RaycastHit hitInfo, castDistance))
        {
            if (hitInfo.collider.TryGetComponent(out BasicLookInteractionController lookInteraction))
            {
                if(_selectedLookInteraction != lookInteraction)
                {
                    _selectedLookInteraction?.Deselect();
                    _selectedLookInteraction = lookInteraction;
                    _selectedLookInteraction.Select();
                }
            }
            else
            {
                _selectedLookInteraction?.Deselect();
                _selectedLookInteraction = null;
            }
        }
        else
        {
            _selectedLookInteraction?.Deselect();
            _selectedLookInteraction = null;
        }
    }

    private void OnInteract()
    {
        if (!LookIsFine()) return;

        _selectedLookInteraction.OnInteract();
    }

    private void OnTalk()
    {
        if (!LookIsFine()) return;

        _selectedLookInteraction.OnTalk();
    }

    private bool LookIsFine()
    {
        return _gameManager.sceneState.Value == GameManager.SceneState.Gameplay && _selectedLookInteraction != null;
    }
}
