using System;
using UniRx;
using UnityEngine;

public class DayManager : SingletonMonoBehaviourBase<DayManager>
{
    public event Action<int> onPastDay;

    public enum DayState
    {
        NeedOpenDoors = 0,
        SpawnProcess = 1,
        NeedCloseDoors = 2,
        NeedEndDay = 3
    }

    public float DayProgressValue => dayProgressValue;

    [Range(0f, 1f)]
    [SerializeField] private float dayProgressValue = 0.0f;
    [SerializeField] private float allDayDuration = 60 * 10;

    [Header("SkyBox")]
    [Range(0f, 1f)]
    [SerializeField] private float skyboxChangeValue;
    [SerializeField] AnimationCurve skyboxExposureCurve;
    [SerializeField] private AnimationCurve skyboxIntensityCurve;
    [SerializeField] private Gradient skyboxTintColorGradient;
    [Space]
    [SerializeField] private Material daySkybox;
    [SerializeField] private Material nightSkybox;

    [Header("Light")]
    [SerializeField] private AnimationCurve lightIntensityCurve;
    [SerializeField] private Gradient lightColorGradient;
    [Space]
    [SerializeField] private Light sunLight;
    [SerializeField] private GameObject dayReflectionProbesContainer;
    [SerializeField] private GameObject nightReflectionProbesContainer;

    [Space]
    [SerializeField] private Spawner spawner;

    [HideInInspector]
    public readonly ReactiveProperty<DayState> state = new ReactiveProperty<DayState>(DayState.NeedOpenDoors);

    private GameManager _gameManager;
    private QuestsManager _questsManager;
    private SavesManager _savesManager;
    private DoorsManager _doorsManager;
    private AudioManager _audioManager;

    private int _daysCount = 0;

    private IDisposable _stateSubscrition;

#if UNITY_EDITOR
    private void OnValidate()
    {
        UpdateSkybox();
        UpdateLight();
    }
#endif

    private void Awake()
    {
        _savesManager = SavesManager.Instance;

        _gameManager = GameManager.Instance;
        _doorsManager = _gameManager.DoorsManager;
        _questsManager = _gameManager.QuestsManager;

        _audioManager = AudioManager.Instance;
    }

    private void Update()
    {
        UpdateState();
        UpdateSkybox();
        UpdateLight();
    }

    private void OnEnable()
    {
        _stateSubscrition = state.Subscribe(OnStateChanged);
    }

    private void OnDisable()
    {
        _stateSubscrition?.Dispose();
        _stateSubscrition = null;
    }

    private void OnStateChanged(DayState newState)
    {
        //Debug.Log(newState);

        if(newState == DayState.NeedOpenDoors)
        {
            _daysCount++;

            if(_daysCount > 1)
            {
                _questsManager.ChangeProgressQuest(QuestConfig.QuestType.Day1, 1);
                _questsManager.ChangeProgressQuest(QuestConfig.QuestType.Day10, 1);
            }

            dayProgressValue = 0;
            _doorsManager.SetDoorsInteractable(true, Door.DoorState.Opened);

            onPastDay?.Invoke(_daysCount);

            _savesManager.PlayerStats.Save();

            _audioManager.PlayCalmMusic();
        }
        else if(newState == DayState.SpawnProcess)
        {
            spawner.StartSpawn();
        }
        else if(newState == DayState.NeedCloseDoors)
        {
            _doorsManager.SetDoorsInteractable(true, Door.DoorState.Closed);

            _audioManager.PlayActionMusic();
        }
        else if (newState == DayState.NeedEndDay)
        {
            spawner.StopSpawn();
        }
    }

    public void DebugSkipDay()
    {
        state.Value = DayState.NeedEndDay;
        state.Value = DayState.NeedOpenDoors;
    }

    private void UpdateState()
    {
        if (state.Value == DayState.NeedOpenDoors)
        {
            if (_doorsManager.IsAnyDoorHasState(Door.DoorState.Opened))
            {
                state.Value = DayState.SpawnProcess;
            }
        }
        else if (state.Value == DayState.SpawnProcess)
        {
            dayProgressValue = Mathf.Clamp01((dayProgressValue * allDayDuration + Time.deltaTime) / allDayDuration);
            spawner.SetDayValue(dayProgressValue);

            if (dayProgressValue >= 1.0f)
            {
                state.Value = DayState.NeedCloseDoors;
            }
        }
        else if (state.Value == DayState.NeedCloseDoors)
        {
            if (_doorsManager.IsAllDoorHasState(Door.DoorState.Closed))
            {
                state.Value = DayState.NeedEndDay;
            }
        }
    }

    private void UpdateSkybox()
    {
        RenderSettings.skybox = dayProgressValue <= skyboxChangeValue ? daySkybox : nightSkybox;
        RenderSettings.skybox.SetColor("_TintColor", skyboxTintColorGradient.Evaluate(dayProgressValue));
        RenderSettings.skybox.SetFloat("_Exposure", skyboxExposureCurve.Evaluate(dayProgressValue));
        //RenderSettings.ambientIntensity = skyboxIntensityCurve.Evaluate(dayProgressValue);
        DynamicGI.UpdateEnvironment();
    }

    private void UpdateLight()
    {
        sunLight.intensity = lightIntensityCurve.Evaluate(dayProgressValue);
        sunLight.color = lightColorGradient.Evaluate(dayProgressValue);
        sunLight.transform.localRotation = Quaternion.Euler(Mathf.Lerp(0.0f, 180.0f, dayProgressValue), 90.0f, 0.0f);

        //dayReflectionProbesContainer.SetActive(dayProgressValue <= skyboxChangeValue);
        //nightReflectionProbesContainer.SetActive(dayProgressValue > skyboxChangeValue);
    }
}
