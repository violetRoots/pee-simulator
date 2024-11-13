using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class HumanMovementController : MonoBehaviour
{
    [SerializeField] private float stoppingDistance = 0.01f;

    [SerializeField]
    [HideInInspector]
    private Transform _transform;

    [SerializeField]
    [HideInInspector]
    private NavMeshAgent _agent;

    private bool _isStopped;

#if UNITY_EDITOR
    private void OnValidate()
    {
        _transform = transform;
        _agent = GetComponent<NavMeshAgent>();
    }
#endif

    private void Awake()
    {
        NavMeshUtility.GetNavMeshPoint(transform.position, out Vector3 startPos);
        transform.position = startPos;
    }

    public void Stop()
    {
        if(_isStopped) return;

        StopAllCoroutines();
        _agent.isStopped = true;
        //_agent.enabled = false;

        _isStopped = true;
    }

    private void Resume(Action onResume)
    {
        StartCoroutine(ResumeProcess(onResume));
    }

    private IEnumerator ResumeProcess(Action onResume)
    {
        //_agent.enabled = true;

        yield return null;

        _agent.isStopped = false;
        _isStopped = false;

        onResume?.Invoke();
    }

    public bool SetDestination(Vector3 destinationPoint, Action onCompleteAction = null)
    {
        bool canMove = NavMeshUtility.GetNavMeshPoint(destinationPoint, out Vector3 navMeshPoint);

        Resume(() =>
        {
            canMove = _agent.SetDestination(navMeshPoint);

            StartCoroutine(MovementProcess(navMeshPoint, onCompleteAction));
        });
        
        return canMove;
    }

    private IEnumerator MovementProcess(Vector3 destinationPoint, Action onCompleteAction = null)
    {
        var moveEnd = false;
        while (!moveEnd)
        {
            var distance = Vector3.Distance(_agent.transform.position, destinationPoint);

            if(distance > stoppingDistance)
                yield return null; 
            else
                moveEnd = true;
        }

        onCompleteAction?.Invoke();
    }


    public void HardMoveToPoint(Transform point)
    {
        NavMeshUtility.GetNavMeshPoint(point.position, out Vector3 navMeshPoint);

        _transform.position = navMeshPoint;
        _transform.rotation = point.rotation;
    }
}
