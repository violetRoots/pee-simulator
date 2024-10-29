using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HumanRunAwayChecker : MonoBehaviour
{
    public float FearDistance => fearDistance;

    [Layer]
    [SerializeField] private string checkLayer;
    [SerializeField] private float checkTimeInterval = 0.1f;
    [SerializeField] private float checkRadius = 2.5f;
    [SerializeField] private float fearDistance = 3.0f;

    private HashSet<Transform> _zombies = new HashSet<Transform>();
    private bool _checkProcessEnabled = true;

    private void Start()
    {
        StartCoroutine(CheckZombieProcess());
    }

    private void Update()
    {
        //Debug.Log(_zombies.Count);
        //if (IsZombiesDetected())
        //    Debug.Log(GetNearestZombie().name);
    }

    private IEnumerator CheckZombieProcess()
    {
        while (_checkProcessEnabled)
        {
            var layerMask = LayerMask.GetMask(checkLayer);
            var zombieTransforms = Physics.OverlapSphere(transform.position, checkRadius, layerMask).Select(collider => collider.transform);
            zombieTransforms = zombieTransforms.OrderBy(zombie => Vector3.Distance(zombie.position, transform.position));

            _zombies = new HashSet<Transform>(zombieTransforms);

            yield return new WaitForSeconds(checkTimeInterval);
        }
    }

    public bool IsZombiesDetected()
    {
        return GetNearestZombie() != null;
    }

    public Transform GetNearestZombie()
    {
        return _zombies.FirstOrDefault();
    }
}
