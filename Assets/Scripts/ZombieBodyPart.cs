using System;
using UnityEngine;

public class ZombieBodyPart : MonoBehaviour 
{
    [SerializeField] private Transform bone;

    private Rigidbody _meshRb;
    private Collider _collider;

    private void Awake()
    {
        _meshRb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();

        _meshRb.isKinematic = true;
        _collider.enabled = false;
    }

    public void AddExplosionForce(float force, Vector3 position, float radius)
    {
        transform.SetPositionAndRotation(bone.position, bone.rotation);

        _meshRb.isKinematic = false;
        _collider.enabled = true;

        _meshRb.AddExplosionForce(force, position, radius);
    }
}
