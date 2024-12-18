using Common.Localisation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiCircleMenu : MonoBehaviour
{
    [Serializable]
    public class CircleSegmentInfo
    {
        public CircleItemConfig config;
        public Image selectImage;
        public float targetAngle;

        [HideInInspector]
        public float lastUseTime;
        [HideInInspector]
        public bool isUsed;
    }

    [SerializeField] private TranslatedTextMeshPro title;
    [SerializeField] private TextMeshProUGUI price;
    [SerializeField] private Image cooldownImage;

    [SerializeField] private CircleSegmentInfo[] segmentInfos;

    private GameManager _gameManager;
    private PlayerStats _playerStats;

    private CharacterProvider _characterProvider;
    private CharacterInteractionController _characterInteractionController;
    private CharacterBuildController _characterBuildController;
    private CharacterWeaponController _characterWeaponController;

    CircleSegmentInfo _targetSegmentInfo;
    private Vector2 _centerPos;

    private void Awake()
    {
        _gameManager = GameManager.Instance;
        _playerStats = SavesManager.Instance.PlayerStats.Value;

        _characterProvider = GameManager.Instance.CharacterProvider;
        _characterBuildController = _characterProvider.BuildController;
        _characterInteractionController = _characterProvider.InteractionController;
        _characterWeaponController = _characterProvider.WeaponController;

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
        UpdateTargetCircleItem();
    }

    public void ApplySelectedCircleItem()
    {
        if (_targetSegmentInfo == null) return;

        if (_targetSegmentInfo.isUsed && Time.time - _targetSegmentInfo.lastUseTime < _targetSegmentInfo.config.cooldown) return;

        if(_targetSegmentInfo.config.type == CircleItemConfig.CircleType.Automate)
        {
            _characterBuildController.SetItem(_targetSegmentInfo.config);
            _characterInteractionController.SetInteractionMode(CharacterInteractionController.CharacterInteractionMode.Build);
        }
        else if (_targetSegmentInfo.config.type == CircleItemConfig.CircleType.Weapon)
        {
            _characterWeaponController.EnableWeapon(_targetSegmentInfo.config);
        }
    }

    private void UpdateTargetCircleItem()
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

            segmentInfo.selectImage.gameObject.SetActive(false);

            var offset = -60.0f;
            var diff = Mathf.Abs(angle - segmentInfo.targetAngle + offset);

            if (diff < minAngleDiff)
            {
                minAngleDiff = diff;

                _targetSegmentInfo = segmentInfo;
            }
        }

        _targetSegmentInfo.selectImage.gameObject.SetActive(true);

        title.SetKey(_targetSegmentInfo.config.title);
        price.text = $"{_targetSegmentInfo.config.price}";
        price.color = _playerStats.money < _targetSegmentInfo.config.price ? Color.red : Color.green;

        if (_targetSegmentInfo.isUsed)
        {
            var cooldownValue = 1.0f - Mathf.Clamp01((Time.time - _targetSegmentInfo.lastUseTime) / _targetSegmentInfo.config.cooldown);
            cooldownImage.fillAmount = cooldownValue;
        }
        else
        {
            cooldownImage.fillAmount = 0;
        }
    }

    public void SetUsed(CircleItemConfig config)
    {
        var segment = segmentInfos.Where(info => info.config == config).FirstOrDefault();

        segment.lastUseTime = Time.time;
        segment.isUsed = true;
    }
}
