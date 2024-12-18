using NaughtyAttributes;
using System;
using System.Collections;
using UniRx;
using UnityEngine;

public class HumanStateController : MonoBehaviour
{
    public enum HumanState
    {
        None = -1,
        WantsToOrder = 0,
        MakesOrder = 1,
        WantsToSit = 2,
        Sits = 3,
        WantsToLeave = 4,
        RunsAway = 5,
        RunsAwayForever = 6,
        Idle = 7,
        WantsToFight = 8,
        Fight = 9
    }

    [MinMaxSlider(0, 25)]
    [SerializeField] private Vector2 makesOrederTimeBounds;
    [MinMaxSlider(0, 60 * 5)]
    [SerializeField] private Vector2 sitsTimeBounds;
    [MinMaxSlider(0, 25)]
    [SerializeField] private Vector2 timeToRunAwayForeverBounds;
    [MinMaxSlider(0, 25)]
    [SerializeField] private Vector2 idleTimeBounds;
    [MinMaxSlider(0, 25)]
    [SerializeField] private Vector2 fightTimeBounds;
    [MinMaxSlider(0, 60 * 5)]
    [SerializeField] private Vector2 leaveTimeBounds;

    [SerializeField] private float fightPositionOffset = 1.0f;

    [SerializeField] private HumanLeaveTimerPanel leaveTimerPanel;

    private GameManager _gameManager;
    private SitPlaceManager _sitPlaceManager;
    private HumanFightManager _humanFightManager;

    private HumanProvider _humanProvider;
    private HumanMovementController _humanMovementController;
    private HumanAnimationController _humanAnimationController;
    private HumanRunAwayChecker _humanRunAwayChecker;
    private HumanPeeController _humanPeeController;
    private HumanContentController _humanContentController;
    private HumanDetectionController _humanDetectionController;

    public readonly ReactiveProperty<HumanState> state = new ReactiveProperty<HumanState>(HumanState.None);

    private HumanState _savedState;

    private float _initTime;
    private float _timeToRunAwayForever;
    private float _lastRunAwayStateEnabledTime;
    private float _leaveTime;

    private bool _isSitting;

    private HumanStateBehaviourData _behaviourData;

    private IDisposable _humanStateSubscription;

    private void Start()
    {
        _gameManager = GameManager.Instance;
        _sitPlaceManager = _gameManager.SitPlaceManager;
        _humanFightManager = _gameManager.HumanFightManager;

        _humanProvider = GetComponent<HumanProvider>();
        _humanMovementController = _humanProvider.MovementController;
        _humanAnimationController = _humanProvider.AnimationController;
        _humanRunAwayChecker = _humanProvider.RunAwayChecker;
        _humanPeeController = _humanProvider.PeeController;
        _humanContentController = _humanProvider.ContentController;
        _humanDetectionController = _humanProvider.DetectionController;

        _initTime = Time.time;
        _timeToRunAwayForever = UnityEngine.Random.Range(timeToRunAwayForeverBounds.x, timeToRunAwayForeverBounds.y);
        _leaveTime = UnityEngine.Random.Range(leaveTimeBounds.x, leaveTimeBounds.y);

        state.Value = HumanState.WantsToOrder;
    }

    private void OnEnable()
    {
        _humanStateSubscription = state.Subscribe(OnStateChanged);      
    }

    private void OnDisable()
    {
        _humanStateSubscription?.Dispose();
        _humanStateSubscription = null;
    }

    private void Update()
    {
        UpdateUI();

        if (state.Value == HumanState.RunsAwayForever) return;

        UpdateHumanDetectionLogic();
        UpdateLeaveTimerLogic();
        UpdatePeeLogic();
        UpdateZombieLogic();
    }

    public void SetBehaviourData(Door door, Kassa kassa)
    {
        _behaviourData = new HumanStateBehaviourData
        {
            door = door,
            kassa = kassa
        };
    }

    public void SaveState()
    {
        _savedState = state.Value;
    }

    private void UpdateHumanDetectionLogic()
    {
        if (state.Value == HumanState.RunsAway || state.Value == HumanState.Sits) return;

        if (_humanDetectionController.IsTargetHumanDetected())
        {
            var targetHuman = _humanDetectionController.GetTargetHuman();
            var targetHumanStateController = targetHuman.StateController;

            if (targetHumanStateController.state.Value == HumanState.WantsToFight ||
                targetHumanStateController.state.Value == HumanState.Fight) return;

            if(state.Value != HumanState.Fight && _humanFightManager.IsTypeStartsFight(_humanContentController.HumanType, targetHuman.ContentController.HumanType))
            {
                state.Value = HumanState.WantsToFight;

                targetHumanStateController.state.Value = HumanState.Fight;
            }
        }
    }

    private void UpdateZombieLogic()
    {
        if (_humanRunAwayChecker.IsZombiesDetected())
        {
            if (state.Value != HumanState.RunsAway)
            {
                _humanMovementController.Stop();
                state.Value = HumanState.RunsAway;
                _lastRunAwayStateEnabledTime = Time.time;
            }

            if (Time.time - _lastRunAwayStateEnabledTime >= _timeToRunAwayForever)
            {
                _humanMovementController.Stop();
                state.Value = HumanState.RunsAwayForever;
                return;
            }

            var zombie = _humanRunAwayChecker.GetNearestZombie();
            _humanMovementController.SetDestination((transform.position - zombie.position).normalized * (_humanRunAwayChecker.FearDistance));
        }
        else if (!_humanRunAwayChecker.IsZombiesDetected() && state.Value == HumanState.RunsAway)
        {
            _humanMovementController.Stop();
            state.Value = HumanState.Idle;
        }
    }

    private void UpdatePeeLogic()
    {
        if(Mathf.Approximately(_humanPeeController.HappyValue, 1.0f))
        {
            state.Value = HumanState.WantsToLeave;
        }
    }

    private void UpdateLeaveTimerLogic()
    {
        if (Time.time - _initTime >= _leaveTime)
        {
            state.Value = HumanState.RunsAwayForever;
        }
    }

    private void UpdateUI()
    {
        var leaveTimerValue = 1.0f - Mathf.Clamp01((Time.time - _initTime) / _leaveTime);
        leaveTimerPanel.UpdateValue(leaveTimerValue);
    }

    private void OnStateChanged(HumanState newState)
    {
        //Debug.Log(newState);
        StopAllCoroutines();
        gameObject.Stop3DSound();

        if(newState != HumanState.Sits && _isSitting)
        {
            StandUp();
        }

        if(newState == HumanState.WantsToOrder)
        {
            _humanMovementController.SetDestination(_behaviourData.kassa.GetPositionForOrder(), () => state.Value = HumanState.MakesOrder);
            _humanAnimationController.PlayWalkAnimation();
        }
        else if (newState == HumanState.MakesOrder)
        {
            DelayedAction(UnityEngine.Random.Range(makesOrederTimeBounds.x, makesOrederTimeBounds.y), () =>
            {
                _humanPeeController.InitOrder();

                state.Value = HumanState.WantsToSit;
            });
            _humanAnimationController.PlayIdleAnimation();
        }
        else if (newState == HumanState.WantsToSit)
        {
            if(!_sitPlaceManager.TryGetAvaialableSitPlace(out SitPlaceManager.SitPlaceGameplayInfo sitPlaceInfo))
            {
                state.Value = HumanState.WantsToLeave;
                return;
            }

            _sitPlaceManager.TakeSitPlace(sitPlaceInfo, _humanProvider);

            _humanMovementController.SetDestination(sitPlaceInfo.place.GetInteractPoint().position, () => Sit(sitPlaceInfo));
            _humanAnimationController.PlayWalkAnimation();
        }
        else if (newState == HumanState.Sits)
        { 
            _humanAnimationController.PlaySitAnimation();
        }
        else if (newState == HumanState.WantsToLeave)
        {
            var exitPoint = _behaviourData.door.GetEntrancePoint();
            _humanMovementController.SetDestination(exitPoint.position, () => Destroy(gameObject));
            _humanAnimationController.PlayWalkAnimation();
        }
        else if (newState == HumanState.RunsAway)
        {
            _humanAnimationController.PlayWalkAnimation();
        }
        else if (newState == HumanState.RunsAwayForever)
        {
            var exitPoint = _behaviourData.door.GetEntrancePoint();
            _humanMovementController.SetDestination(exitPoint.position, () => Destroy(gameObject));
            _humanAnimationController.PlayWalkAnimation();
        }
        else if (newState == HumanState.Idle)
        {
            DelayedAction(UnityEngine.Random.Range(idleTimeBounds.x, idleTimeBounds.y), () => state.Value = _savedState);
            _humanAnimationController.PlayIdleAnimation();
        }
        else if (newState == HumanState.WantsToFight)
        {
            var humanPos = _humanDetectionController.GetTargetHuman().transform.position;
            var fightDir = humanPos - transform.position;
            var fightPos = humanPos - fightDir.normalized * fightPositionOffset;

            _humanMovementController.SetPosition(fightPos);
            _humanMovementController.SetRotation(Quaternion.LookRotation(fightDir));

            state.Value = HumanState.Fight;

            //_humanMovementController.SetDestination(fightPos, () =>
            //{
            //    state.Value = HumanState.Fight;
            //});
        }
        else if (newState == HumanState.Fight)
        {
            _humanMovementController.Stop();

            _humanContentController.FightCollider.gameObject.SetActive(true);

            DelayedAction(UnityEngine.Random.Range(fightTimeBounds.x, fightTimeBounds.y), () => state.Value = HumanState.RunsAwayForever);
            _humanAnimationController.PlayFightAnimation();

            gameObject.Play3DSound(SfxType.PeopleFight, 0.5f);
        }
    }

    private void DelayedAction(float time, Action action)
    {
        StartCoroutine(DelayedActionProcess(time, action));
    }

    private IEnumerator DelayedActionProcess(float time, Action action)
    {
        yield return new WaitForSeconds(time);

        action?.Invoke();
    }

    private void Sit(SitPlaceManager.SitPlaceGameplayInfo sitPlaceInfo)
    {
        _humanMovementController.Stop();
        _humanMovementController.HardMoveToPoint(sitPlaceInfo.place.GetSitPoint());
        state.Value = HumanState.Sits;

        _isSitting = true;
    }

    private void StandUp()
    {
        var sitPlaceInfo = _sitPlaceManager.FreeSitPlace(_humanProvider);
        _humanMovementController.HardMoveToPoint(sitPlaceInfo.place.GetInteractPoint());
        state.Value = HumanState.WantsToLeave;

        _isSitting = false;
    }
}

public class HumanStateBehaviourData
{
    public Door door;
    public Kassa kassa;
}
