using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestStateMarker : MonoBehaviour
{
    [SerializeField] private RectTransform marker;
    [Space]
    [SerializeField] private float screenEdgePadding = 10.0f;

    private Camera _mainCamera;

    private QuestsManager _questsManager;

    private RectTransform _markerIcon;

    private bool _isActive;
    private Vector2 _screenSize;
    private Vector2 _screenFullHd;

    private void Awake()
    {
        _mainCamera = Camera.main;

        _questsManager = GameManager.Instance.QuestsManager;

        _markerIcon = UiGameplayManager.Instance.InstatiateMarker(marker);

        _screenSize = new Vector2(Screen.width, Screen.height);
        _screenFullHd = new Vector2(1920, 1080);

        UpdateMarkerVisability();
    }

    private void OnEnable()
    {
        _questsManager.onQuestProgressUpdated += OnQuestStateChanged;
    }

    private void OnDisable()
    {
        _questsManager.onQuestProgressUpdated -= OnQuestStateChanged;
    }

    private void OnQuestStateChanged(QuestRuntimeInfo questInfo)
    {
        UpdateMarkerVisability();
    }

    private void UpdateMarkerVisability()
    {
        var isAnyQuestFinished = _questsManager.GetQuests().Any(q => q.isFinished && !q.isOpened);
        SetIsActive(isAnyQuestFinished);
    }

    private void Update()
    {
        if (!_isActive || !IsMarkerVisible()) return;

        Debug.Log(_isActive);

        UpdateMarkerPos();
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
        if (isVisible || !isVisible && !isActive)
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
