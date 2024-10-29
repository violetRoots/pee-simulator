using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class ZombieMovementController : MonoBehaviour
{
    [SerializeField] private float yOffset;
    [SerializeField] private float stoppingDistance = 0.1f;

    [SerializeField]
    [HideInInspector]
    private ZombieProvider _provider;

    [SerializeField]
    [HideInInspector]
    private NavMeshAgent _agent;

#if UNITY_EDITOR
    private void OnValidate()
    {
        _provider = GetComponent<ZombieProvider>();
        _agent = GetComponent<NavMeshAgent>();
    }
#endif

    public void Stop()
    {
        StopAllCoroutines();
        _agent.isStopped = true;
    }

    public bool SetDestination(Vector3 destinationPoint)
    {
        _agent.isStopped = false;
        return _agent.SetDestination(destinationPoint);
    }

    public bool SetDestination(Vector3 destinationPoint, Action onCompleteAction)
    {
        _agent.isStopped = false;
        var canMove = _agent.SetDestination(destinationPoint);

        StartCoroutine(MovementProcess(destinationPoint, stoppingDistance, onCompleteAction));

        return canMove;
    }

    public bool SetDestination(Vector3 destinationPoint, float customStoppingDistance, Action onCompleteAction)
    {
        _agent.isStopped = false;
        var canMove = _agent.SetDestination(destinationPoint);

        StartCoroutine(MovementProcess(destinationPoint, customStoppingDistance, onCompleteAction));

        return canMove;
    }

    private IEnumerator MovementProcess(Vector3 destinationPoint, float stoppingDistance, Action onCompleteAction)
    {
        var moveEnd = false;
        while (!moveEnd)
        {
            var distance = Vector3.Distance(_agent.transform.position, destinationPoint);

            if (distance > stoppingDistance)
                yield return null;
            else
                moveEnd = true;
        }

        onCompleteAction?.Invoke();
    }

    public void RotateTowards(Vector3 target)
    {
        var rotation = transform.rotation.eulerAngles;
        transform.LookAt(target, Vector3.up);
        transform.rotation = Quaternion.Euler(rotation.x, transform.rotation.eulerAngles.y, rotation.z);
    }
}
