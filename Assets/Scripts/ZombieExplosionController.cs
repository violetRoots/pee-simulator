using System.Collections;
using System.Collections.Generic;
using UniRx;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class ZombieExplosionController : MonoBehaviour
{
    [SerializeField] private Transform bonesParent;
    [SerializeField] private ZombieBodyPart[] bodyParts;

    [SerializeField] private float explosionForce = 100.0f;
    [SerializeField] private float explosionRadius = 1.5f;

    [SerializeField] private GameObject[] bloodFX;

    [SerializeField] private GameObject zombieObj;
    [SerializeField] private GameObject zombiePartsObj;

    private Collider[] _ragDollColliders;

    [SerializeField]
    [HideInInspector]
    private ZombieProvider _zombieProvider;

#if UNITY_EDITOR
    private void OnValidate()
    {
        _zombieProvider = GetComponent<ZombieProvider>();
    }
#endif

    private void Awake()
    {
        _ragDollColliders = bonesParent.GetComponentsInChildren<Collider>();

        foreach (var ragDollCollider in _ragDollColliders)
        {
            ragDollCollider.enabled = false;
        }

        zombieObj.SetActive(true);
        zombiePartsObj.SetActive(false);
    }

    public void Exlode(Vector3 explosionPos)
    {
        if (_zombieProvider.StateController.state.Value == ZombieStateController.ZombieState.Dead) return;

        _zombieProvider.StateController.state.Value = ZombieStateController.ZombieState.Dead;

        zombieObj.SetActive(false);
        zombiePartsObj.SetActive(true);

        foreach (var part in bodyParts)
        {
            part.AddExplosionForce(explosionForce, explosionPos, explosionRadius);
            BloodEffectActivate(part.transform.position);
        }
    }

    private void BloodEffectActivate(Vector3 pos)
    {
        var effectIdx = Random.Range(0, bloodFX.Length);

        var instance = Instantiate(bloodFX[effectIdx], pos, Quaternion.Euler(0.0f, Random.Range(0.0f, 360.0f), 0.0f));
    }
}
