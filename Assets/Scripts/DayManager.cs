using System;
using UniRx;
using UnityEngine;

public class DayManager : SingletonMonoBehaviourBase<DayManager>
{
    public event Action onPastDay;

    public enum DayState
    {
        NeedOpenDoors = 0,
        SpawnProcess = 1,
        NeedCloseDoors = 2,
        NeedEndDay = 3
    }

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

    private SavesManager _dataManager;
    private DoorsManager _doorsManager;

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
        _dataManager = SavesManager.Instance;
        _doorsManager = GameManager.Instance.DoorsManager;
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
            dayProgressValue = 0;
            _doorsManager.SetDoorsInteractable(true, Door.DoorState.Opened);

            onPastDay?.Invoke();

            _dataManager.PlayerStats.Save();
        }
        else if(newState == DayState.SpawnProcess)
        {
            spawner.StartSpawn();
        }
        else if(newState == DayState.NeedCloseDoors)
        {
            _doorsManager.SetDoorsInteractable(true, Door.DoorState.Closed);
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
