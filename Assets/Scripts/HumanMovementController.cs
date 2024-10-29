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
        bool canMove = false;

        Resume(() =>
        {
            canMove = _agent.SetDestination(destinationPoint);

            StartCoroutine(MovementProcess(destinationPoint, onCompleteAction));
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
        _transform.position = point.position;
        _transform.rotation = point.rotation;
    }
}
