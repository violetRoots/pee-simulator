using System;
using System.Collections;
using UniRx;
using UnityEngine;

public class DayStateMarker : MonoBehaviour
{
    [SerializeField] private DayManager.DayState activationDayState;
    [SerializeField] private DayManager.DayState deactivationDayState;
    [SerializeField] private RectTransform marker;
    [Space]
    [SerializeField] private float screenEdgePadding = 10.0f;

    private Camera _mainCamera;

    private DayManager _dayManager;

    private RectTransform _markerIcon;

    private bool _isActive;
    private Vector2 _screenSize;
    private Vector2 _screenFullHd;

    private IDisposable _dayStateSubscription;

    private void Awake()
    {
        _mainCamera = Camera.main;

        _dayManager = DayManager.Instance;

        _markerIcon = UiGameplayManager.Instance.InstatiateMarker(marker);

        _screenSize = new Vector2(Screen.width, Screen.height);
        _screenFullHd = new Vector2(1920, 1080);

        SetIsActive(false);
    }

    private void OnEnable()
    {
        _dayStateSubscription = _dayManager.state.Subscribe(OnDayStateChanged);
    }

    private void OnDisable()
    {
        _dayStateSubscription?.Dispose();
        _dayStateSubscription = null;
    }

    private void Update()
    {
        if (!_isActive || !IsMarkerVisible()) return;

        UpdateMarkerPos();
    }

    private void OnDayStateChanged(DayManager.DayState dayState)
    {
        if (dayState == activationDayState)
            SetIsActive(true);
        if(dayState == deactivationDayState)
            SetIsActive(false);

    }

    private void UpdateMarkerPos()
    {
        Vector2 screenPos = _mainCamera.WorldToScreenPoint(transform.position);
        screenPos.x = Mathf.Clamp(screenPos.x, screenEdgePadding, _screenSize.x - screenEdgePadding);
        screenPos.y = Mathf.Clamp(screenPos.y, screenEdgePadding, _screenSize.y - screenEdgePadding);

        screenPos = screenPos / _screenSize * _screenFullHd;

        _markerIcon.anchoredPosition = screenPos;
    }

    public void SetIsActive(bool isActive)
    {
        StopAllCoroutines();

        _isActive = isActive;

        var isVisible = IsMarkerVisible();
        if(isVisible || !isVisible && !isActive)
            _markerIcon.gameObject.SetActive(isActive);
        else
            StartCoroutine(MarkerChangeVisabilityProcess(isActive));
    }

    private IEnumerator MarkerChangeVisabilityProcess(bool isActive)
    {
        yield return new WaitUntil(() => IsMarkerVisible());

        _markerIcon.gameObject.SetActive(isActive);
    }

    private bool IsMarkerVisible()
    {
        var markerDir = (transform.position - _mainCamera.transform.position).normalized;
        bool isVisible = Vector3.Dot(_mainCamera.transform.forward, markerDir) > 0;

        return isVisible;
    }
}
