using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class HumanStateController : MonoBehaviour
{
    public enum HumanState
    {
        WantsToOrder = 0,
        MakesOrder = 1,
        WantsToSit = 2,
        Sits = 3,
        WantsToLeave = 4,
        RunsAway = 5,
        RunsAwayForever = 6,
        Idle = 7,
    }

    [MinMaxSlider(0, 25)]
    [SerializeField] private Vector2 makesOrederTimeBounds;
    [MinMaxSlider(0, 60*5)]
    [SerializeField] private Vector2 sitsTimeBounds;
    [MinMaxSlider(0, 25)]
    [SerializeField] private Vector2 timeToRunAwayForeverBounds;
    [MinMaxSlider(0, 25)]
    [SerializeField] private Vector2 idleTimeBounds;

    private GameManager _gameManager;
    private SitPlaceManager _sitPlaceManager;
    private HumanProvider _humanProvider;
    private HumanMovementController _humanMovementController;
    private HumanAnimationController _humanAnimationController;
    private HumanRunAwayChecker _humanRunAwayChecker;
    private HumanPeeController _humanPeeController;

    private readonly ReactiveProperty<HumanState> state = new ReactiveProperty<HumanState>(HumanState.WantsToOrder);

    private HumanState _savedState;

    private float _timeToRunAwayForever;
    private float _lastRunAwayStateEnabledTime;

    private IDisposable _stateSubscription;

    private void Awake()
    {
        _gameManager = GameManager.Instance;
        _sitPlaceManager = _gameManager.SitPlaceManager;

        _humanProvider = GetComponent<HumanProvider>();
        _humanMovementController = _humanProvider.MovementController;
        _humanAnimationController = _humanProvider.AnimationController;
        _humanRunAwayChecker = _humanProvider.RunAwayChecker;
        _humanPeeController = _humanProvider.PeeController;

        _timeToRunAwayForever = UnityEngine.Random.Range(timeToRunAwayForeverBounds.x, timeToRunAwayForeverBounds.y);
    }

    private void OnEnable()
    {
        _stateSubscription = state.Subscribe(OnStateChanged);        
    }

    private void OnDisable()
    {
        _stateSubscription?.Dispose();
        _stateSubscription = null;
    }

    private void Update()
    {
        if (state.Value == HumanState.RunsAwayForever) return;

        if (_humanRunAwayChecker.IsZombiesDetected())
        {
            if(state.Value != HumanState.RunsAway)
            {
                _humanMovementController.Stop();
                state.Value = HumanState.RunsAway;
                _lastRunAwayStateEnabledTime = Time.time;
            }

            if(Time.time - _lastRunAwayStateEnabledTime >= _timeToRunAwayForever)
            {
                _humanMovementController.Stop();
                state.Value = HumanState.RunsAwayForever;
                return;
            }

            var zombie = _humanRunAwayChecker.GetNearestZombie();
            _humanMovementController.SetDestination((transform.position - zombie.position).normalized * (_humanRunAwayChecker.FearDistance));
        }
        else if(!_humanRunAwayChecker.IsZombiesDetected() && state.Value == HumanState.RunsAway)
        {
            _humanMovementController.Stop();
            state.Value = HumanState.Idle;
        }
    }

    private void OnStateChanged(HumanState newState)
    {
        //Debug.Log(newState);

        if(newState == HumanState.WantsToOrder)
        {
            _humanMovementController.SetDestination(_gameManager.KassaManager.GetRandomKassa().GetPositionForOrder(), () => state.Value = HumanState.MakesOrder);
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

            _humanMovementController.SetDestination(sitPlaceInfo.place.GetInteractPoint().position, () =>
            {
                _humanMovementController.Stop();
                _humanMovementController.HardMoveToPoint(sitPlaceInfo.place.GetSitPoint());
                state.Value = HumanState.Sits;
            });
            _humanAnimationController.PlayWalkAnimation();
        }
        else if (newState == HumanState.Sits)
        {
            DelayedAction(UnityEngine.Random.Range(sitsTimeBounds.x, sitsTimeBounds.y), () =>
            {
                var sitPlaceInfo = _sitPlaceManager.FreeSitPlace(_humanProvider);
                _humanMovementController.HardMoveToPoint(sitPlaceInfo.place.GetInteractPoint());
                state.Value = HumanState.WantsToLeave;
            });
            _humanAnimationController.PlaySitAnimation();
        }
        else if (newState == HumanState.WantsToLeave)
        {
            var exitPoint = _gameManager.ExitPointsManager.GetRandomExitPoint();
            _humanMovementController.SetDestination(exitPoint.position, () => Destroy(gameObject));
            _humanAnimationController.PlayWalkAnimation();
        }
        else if (newState == HumanState.RunsAway)
        {
            _humanAnimationController.PlayWalkAnimation();
        }
        else if (newState == HumanState.RunsAwayForever)
        {
            var exitPoint = _gameManager.ExitPointsManager.GetRandomExitPoint();
            _humanMovementController.SetDestination(exitPoint.position, () => Destroy(gameObject));
            _humanAnimationController.PlayWalkAnimation();
        }
        else if (newState == HumanState.Idle)
        {
            DelayedAction(UnityEngine.Random.Range(idleTimeBounds.x, idleTimeBounds.y), () => state.Value = _savedState);
            _humanAnimationController.PlayIdleAnimation();
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
}
