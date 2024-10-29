using NaughtyAttributes;
using System;
using System.Collections;
using UniRx;
using UnityEngine;

public class ZombieStateController : MonoBehaviour
{
    public enum ZombieState
    {
        Idle = 0,
        Walk = 1,
        Agression = 2,
        Attack = 3,
        Die = 4,
    }
    
    public readonly ReactiveProperty<ZombieState> state = new ReactiveProperty<ZombieState>(ZombieState.Walk);

    [MinMaxSlider(0, 25)]
    [SerializeField] private Vector2 zombieIdleTimeBounds;
    [SerializeField] private float attackTime = 1.0f;
    [SerializeField] private float dieTime = 1.0f;
    [SerializeField] private float attackStoppingDistance = 0.5f;
    [SerializeField] private float cathTime = 5.0f;

    [SerializeField]
    [HideInInspector]
    private ZombieProvider _zombieProvider;
    private ZombieMovementController _zombieMovementController;
    private ZombieAnimationController _zombieAnimationController;
    private ZombieDetectionController _zombieDetectionController;

    private GameManager _gameManager;

    private Transform _target;
    private float _lastDetectedTime;

    private IDisposable _stateSuscription;

#if UNITY_EDITOR
    private void OnValidate()
    {
        _zombieProvider = GetComponent<ZombieProvider>();
    }
#endif

    private void Awake()
    {
        _zombieMovementController = _zombieProvider.MovementController;
        _zombieAnimationController = _zombieProvider.AnimationController;
        _zombieDetectionController = _zombieProvider.DetectionController;

        _gameManager = GameManager.Instance;
    }

    private void OnEnable()
    {
        _stateSuscription = state.Subscribe(OnStateChanged);
    }

    private void OnDisable()
    {
        _stateSuscription?.Dispose();
        _stateSuscription = null;
    }

    private void Update()
    {
        if (state.Value == ZombieState.Attack || state.Value == ZombieState.Die) return;

        if (_zombieDetectionController.IsTargetDetected())
        {
            if(state.Value != ZombieState.Agression)
            {
                _zombieMovementController.Stop();
                state.Value = ZombieState.Agression;
            }

            _target = _zombieDetectionController.GetTarget();
            _zombieMovementController.SetDestination(_target.position, attackStoppingDistance, () =>
            {
                state.Value = ZombieState.Attack;
                _zombieMovementController.RotateTowards(_target.position);
            });
            _lastDetectedTime = Time.time;
        }
        else if (state.Value == ZombieState.Agression && !_zombieDetectionController.IsTargetDetected())
        {
            if(_target == null)
            {
                state.Value = ZombieState.Walk;
            }

            if(Time.time - _lastDetectedTime >= cathTime)
            {
                state.Value = ZombieState.Walk;
            }
        }
    }

    private void OnStateChanged(ZombieState newState)
    {
        Debug.Log(newState);

        if(newState == ZombieState.Idle)
        {
            _zombieMovementController.Stop();
            _zombieAnimationController.PlayIdleAnimation();
            DelayedAction(UnityEngine.Random.Range(zombieIdleTimeBounds.x, zombieIdleTimeBounds.y), () => state.Value = ZombieState.Walk);
        }
        else if(newState == ZombieState.Walk)
        {
            _zombieAnimationController.PlayWalkAnimation();
            _zombieMovementController.SetDestination(_gameManager.ZombiePointsManager.GetRandomPoint().position, () => state.Value = ZombieState.Idle);
        }
        else if(newState == ZombieState.Agression)
        {
            _zombieAnimationController.PlayWalkAnimation();
        }
        else if(newState == ZombieState.Attack)
        {
            _zombieMovementController.Stop();
            _zombieAnimationController.PlayAttackAnimation();
            DelayedAction(attackTime, () => state.Value = ZombieState.Walk);

        }
        else if (newState == ZombieState.Die)
        {
            _zombieMovementController.Stop();
            _zombieAnimationController.PlayDieAnimation();
            DelayedAction(dieTime, () => Destroy(gameObject));

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
