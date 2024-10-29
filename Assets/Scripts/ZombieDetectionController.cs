using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ZombieDetectionController : MonoBehaviour
{
    [Layer]
    [SerializeField] private string humanLayer;

    [Layer]
    [SerializeField] private string characterLayer;

    [SerializeField] private Vector3 castOriginOffset;
    [SerializeField] private float castDistance = 1.0f;
    [SerializeField] private float castRadius = 1.0f;
    [SerializeField] private float castTimeInterval = 0.01f;

    [SerializeField]
    [HideInInspector]
    private ZombieProvider _provider;

    private Transform _target;
    private bool _detectionProcessEnabled = true;

#if UNITY_EDITOR
    private void OnValidate()
    {
        _provider = GetComponent<ZombieProvider>();
    }

    private void OnDrawGizmos()
    {
        //Debug.DrawRay(transform.position + castOriginOffset, transform.forward * castDistance);
    }
#endif

    private void Start()
    {
        StartCoroutine(DetectionProcess());
    }

    private IEnumerator DetectionProcess()
    {
        while (_detectionProcessEnabled)
        {
            Detect2();

            yield return new WaitForSeconds(castTimeInterval);
        }
    }

    private void Detect()
    {
        var layer = LayerMask.GetMask(characterLayer, humanLayer);
        if (Physics.SphereCast(transform.position + castOriginOffset, castRadius, transform.forward, out RaycastHit hit, castDistance, layer))
        {
            Debug.Log(hit.collider.name);
            _target = hit.collider.transform;
        }
        else
        {
            _target = null;
        }
    }

    private void Detect2()
    {
        var layer = LayerMask.GetMask(characterLayer, humanLayer);
        var colliderTransforms = Physics.OverlapSphere(transform.position + castOriginOffset, castRadius, layer).Select(collider => collider.transform);
        colliderTransforms.OrderBy(t => Vector3.Distance(t.position, transform.position));

        _target = colliderTransforms.FirstOrDefault();
    }

    public bool IsTargetDetected()
    {
        return _target != null;
    }

    public Transform GetTarget()
    {
        return _target;
    }
}
