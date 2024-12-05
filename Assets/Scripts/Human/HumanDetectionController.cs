using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HumanDetectionController : MonoBehaviour
{
    [Layer]
    [SerializeField] private string humanLayer;

    [SerializeField] private Vector3 castOriginOffset;
    [SerializeField] private float castRadius = 1.0f;
    [SerializeField] private float castTimeInterval = 0.01f;

    private HumanProvider _targetHuman;
    private bool _detectionProcessEnabled = true;

#if UNITY_EDITOR
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
            Detect();

            yield return new WaitForSeconds(castTimeInterval);
        }
    }

    private void Detect()
    {
        var layer = LayerMask.GetMask(humanLayer);
        var colliderTransforms = Physics.OverlapSphere(transform.position + castOriginOffset, castRadius, layer).Select(collider => collider.transform);
        colliderTransforms.OrderBy(t => Vector3.Distance(t.position, transform.position));
        var target = colliderTransforms.FirstOrDefault();

        if (target == null) return;

        _targetHuman = target.GetComponent<HumanProvider>();
    }

    public bool IsTargetHumanDetected()
    {
        return _targetHuman != null;
    }

    public HumanProvider GetTargetHuman()
    {
        return _targetHuman;
    }
}
