using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
public class HumanBloodSpawner : MonoBehaviour
{
    //public HumanType Type { get; private set; }

    [SerializeField] private Collider selfCollider;
    //[SerializeField] private HumanContentType humanContentType;

    private HumanFightManager _humanFightManager;

#if UNITY_EDITOR
    private void OnValidate()
    {
        var sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.radius = 0.1f;
        sphereCollider.center = new Vector3(-0.05f, 0.0f, 0.0f);

        var rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeAll;

        //humanContentType = GetComponentInParent<HumanContentType>();
        //Type = humanContentType.HumanType;
    }
#endif
    private void Awake()
    {
        _humanFightManager = GameManager.Instance.HumanFightManager;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider == selfCollider) return;

        if (LayerMask.LayerToName(collision.gameObject.layer) != _humanFightManager.FightLayer) return;

        if (collision.collider.TryGetComponent(out HumanBloodSpawner _)) return;

        var bloodEffects = _humanFightManager.GetBloodEffects();

        Instantiate(bloodEffects[Random.Range(0, bloodEffects.Length)], collision.contacts[0].point, Quaternion.LookRotation(collision.contacts[0].normal));
    }
}
