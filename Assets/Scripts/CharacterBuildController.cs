using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using NaughtyAttributes;
using Common.Utils;
using System.Linq;

public class CharacterBuildController : MonoBehaviour
{
    [Layer]
    [SerializeField] private string layerMaskName;
    [SerializeField] private float raycastDistance = 3.0f;
    [SerializeField] private float raycastRadius = 0.2f;

    [Space]
    [SerializeField] private float moveDampTime = 0.1f;
    [SerializeField] private float rotationDampTime = 0.1f;
    [SerializeField] private Vector3 contentRotationOffset;

    [Space]
    [SerializeField] private float scrollSensitivity = 0.1f;

    [Space]
    [SerializeField] private GameObject wholeBuildContainer;

    [SerializeField] private Transform raycastOrigin;

    [SerializeField] private Transform buildTarget;
    [SerializeField] private Transform deafultTargetPositionAndRotation;

    private GameManager _gameManager;
    private InputManager _inputManager;
    private CharacterInteractionController _characterInteractionController;

    private bool _canBuild;
    private bool _canPlace;
    private Vector3 _targetPos;
    private float _targetOffset;
    private Vector3 _movementDampVelocity;
    private Quaternion _targetRotation;
    private Quaternion _rotationDampVelocity;
    private Vector3 _offsetEuler;

    private LayerMask _layerMask;

    private CircleItemConfig _currentItemToBuild;
    private PeeAutomate _content;

    private IDisposable _changeModeSubscription;

    private void Awake()
    {
        _gameManager = GameManager.Instance;
        _inputManager = InputManager.Instance;
        _characterInteractionController = GameManager.Instance.CharacterProvider.InteractionController;

        _layerMask = LayerMask.GetMask(layerMaskName);
    }

    private void OnEnable()
    {
        _inputManager.OnLeftMouseDown += OnPlaceButtonPressed;
        _changeModeSubscription = _characterInteractionController.interactionMode.Subscribe(OnInteractionModeChanged);
    }

    private void OnDisable()
    {
        if(_inputManager != null)
            _inputManager.OnLeftMouseDown -= OnPlaceButtonPressed;

        _changeModeSubscription?.Dispose();
        _changeModeSubscription = null;
    }

    private void Update()
    {
        if (!_canBuild) return;

        var scrollDelta = Input.mouseScrollDelta.y;
        if (Mathf.Abs(scrollDelta) > scrollSensitivity)
        {
            _offsetEuler += new Vector3(0.0f, 0.0f, Mathf.Sign(scrollDelta) * 90.0f);
        }

        if(Physics.SphereCast(raycastOrigin.position, raycastRadius, raycastOrigin.forward, out RaycastHit hit, raycastDistance, _layerMask))
        {
            _targetPos = hit.point;
            _targetPos += hit.normal * _targetOffset;
            _targetRotation = Quaternion.LookRotation(hit.normal) * Quaternion.Euler(_offsetEuler);

            _canPlace = true;
        }
        else
        {
            _targetPos = deafultTargetPositionAndRotation.position;
            _targetRotation = deafultTargetPositionAndRotation.rotation;

            _canPlace = false;
        }

        buildTarget.rotation = QuaternionUtil.SmoothDamp(buildTarget.rotation, _targetRotation, ref _rotationDampVelocity, rotationDampTime); 
        buildTarget.position = Vector3.SmoothDamp(buildTarget.position, _targetPos, ref _movementDampVelocity, moveDampTime);

        UpdateCantPlaceMarker();
    }

    private void OnInteractionModeChanged(CharacterInteractionController.CharacterInteractionMode interactionMode)
    {
        _canBuild = interactionMode == CharacterInteractionController.CharacterInteractionMode.Build ? true : false;

        wholeBuildContainer.SetActive(_canBuild);

        if(!_canBuild) return;

        SetContent();
    }

    private void OnPlaceButtonPressed()
    {
        if (!_gameManager.IsGameplayInputEnabled()) return;

        if (_characterInteractionController.interactionMode.Value != CharacterInteractionController.CharacterInteractionMode.Build) return;

        if (_currentItemToBuild == null || _gameManager.Data.Money < _currentItemToBuild.price) return;

        if (!_canBuild || !_canPlace) return;

        var sector = GetGameplaySector(_content.transform.position);
        _content.Activate(sector, _currentItemToBuild);
        _content = null;

        _gameManager.Data.ChangeMoney(-_currentItemToBuild.price);

        SetContent();
    }

    public void SetItem(CircleItemConfig item)
    {
        _currentItemToBuild = item;

        SetContent();
    }

    private void SetContent()
    {
        if (_content != null)
        {
            Destroy(_content.gameObject);
        }

        _content = Instantiate(_currentItemToBuild.automate, buildTarget.position, buildTarget.rotation * _currentItemToBuild.automate.transform.rotation, buildTarget);
        _targetOffset = 0.0F;// -content.GetAoutomateSizeCollider().bounds.size.y;
    }

    private void UpdateCantPlaceMarker()
    {
        _content.SetPlaceMarkerActive(_canPlace);
        _content.SetPlaceMarkerMaterial(_currentItemToBuild != null && _gameManager.Data.Money >= _currentItemToBuild.price);
    }

    private GameplaySector GetGameplaySector(Vector3 pos)
    {
        return _gameManager.GameplaySectors.Where(sector => sector.Contains(pos)).FirstOrDefault();
    }
}
