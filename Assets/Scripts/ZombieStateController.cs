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
    private ZombieMovementController _movementController;
    private ZombieAnimationController _animationController;
    private ZombieDetectionController _detectionController;
    private ZombieAttackController _attackController;

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
        _movementController = _zombieProvider.MovementController;
        _animationController = _zombieProvider.AnimationController;
        _detectionController = _zombieProvider.DetectionController;
        _attackController = _zombieProvider.AttackController;

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

        if (_detectionController.IsTargetDetected())
        {
            if(state.Value != ZombieState.Agression)
            {
                _movementController.Stop();
                state.Value = ZombieState.Agression;
            }

            _target = _detectionController.GetTarget();
            _movementController.SetDestination(_target.position, attackStoppingDistance, () =>
            {
                state.Value = ZombieState.Attack;
                _attackController.Attack();
                _movementController.RotateTowards(_target.position);
            });
            _lastDetectedTime = Time.time;
        }
        else if (state.Value == ZombieState.Agression && !_detectionController.IsTargetDetected())
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
        //Debug.Log(newState);

        if(newState == ZombieState.Idle)
        {
            _movementController.Stop();
            _animationController.PlayIdleAnimation();
            DelayedAction(UnityEngine.Random.Range(zombieIdleTimeBounds.x, zombieIdleTimeBounds.y), () => state.Value = ZombieState.Walk);
        }
        else if(newState == ZombieState.Walk)
        {
            _animationController.PlayWalkAnimation();
            _movementController.SetDestination(_gameManager.ZombiePointsManager.GetRandomPoint().position, () => state.Value = ZombieState.Idle);
        }
        else if(newState == ZombieState.Agression)
        {
            _animationController.PlayWalkAnimation();
        }
        else if(newState == ZombieState.Attack)
        {
            _movementController.Stop();
            _animationController.PlayAttackAnimation();
            DelayedAction(attackTime, () => state.Value = ZombieState.Walk);

        }
        else if (newState == ZombieState.Die)
        {
            _movementController.Stop();
            _animationController.PlayDieAnimation();
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
