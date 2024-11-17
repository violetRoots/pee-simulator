using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiCircleMenu : MonoBehaviour
{
    [Serializable]
    public class CircleSegmentInfo
    {
        public UiCircleSegment segment;
        public float targetAngle;
    }

    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI price;

    [SerializeField] private CircleSegmentInfo[] segmentInfos;

    private GameManager _gameManager;
    private CharacterInteractionController _characterInteractionController;
    private CharacterBuildController _characterBuildController;

    UiCircleSegment _targetSegment;
    private Vector2 _centerPos;

    private void Awake()
    {
        _gameManager = GameManager.Instance;
        _characterBuildController = GameManager.Instance.CharacterProvider.BuildController;
        _characterInteractionController = GameManager.Instance.CharacterProvider.InteractionController;

        _centerPos = new Vector2(Screen.width / 2, Screen.height / 2);
    }

    private void OnEnable()
    {
        _gameManager.AddLock(this);
    }

    private void OnDisable()
    {
        _gameManager.RemoveLock(this);
    }

    private void Update()
    {
        Vector2 mousePos = Input.mousePosition;
        var direction = (mousePos - _centerPos).normalized;
        var angle = Vector2.SignedAngle(direction, Vector2.up);
        angle = angle < 0 ? 360 - Mathf.Abs(angle) : angle;
        angle = angle < 30.0f ? 360 + angle : angle;

        float minAngleDiff = float.PositiveInfinity;
        for (int i = 0; i < segmentInfos.Length; i++)
        {
            CircleSegmentInfo segmentInfo = segmentInfos[i];

            if (!segmentInfo.segment.CanSelect()) continue;

            segmentInfo.segment.Deselect();

            var offset = -60.0f;
            var diff = Mathf.Abs(angle - segmentInfo.targetAngle + offset);

            if (diff < minAngleDiff)
            {
                minAngleDiff = diff;

                _targetSegment = segmentInfo.segment;
            }
        }

        _targetSegment.Select();

        title.text = _targetSegment.ItemConfig.title;
        price.text = $"{_targetSegment.ItemConfig.price}";
        price.color = _gameManager.Data.Money < _targetSegment.ItemConfig.price ? Color.red : Color.green;
    }

    public void ApplySelectedAutomate()
    {
        if (_targetSegment == null) return;

        _characterBuildController.SetItem(_targetSegment.ItemConfig);
        _characterInteractionController.SetInteractionMode(CharacterInteractionController.CharacterInteractionMode.Build);
    }
}
