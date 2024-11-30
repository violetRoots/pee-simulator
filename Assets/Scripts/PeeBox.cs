using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeeBox : MonoBehaviour
{
    [Serializable]
    public class ExplosionIfno
    {
        public float force = 100.0f;
        public float radius = 1.0f;
    }

    [SerializeField] private ExplosionIfno explosionInfo;
    [SerializeField] private int scoreAddiction = 10;

    [SerializeField] private Transform peeTracePrefab;

    private Rigidbody _rigidbody;
    private SuppliersManager.SupplierRuntimeInfo _supplier;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void PeeForward(Vector3 direction, float stregnth, SuppliersManager.SupplierRuntimeInfo supplier = null)
    {
        _supplier = supplier;

        _rigidbody.AddForce(direction * stregnth);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);

        var contact = collision.contacts[0];
        var direction = contact.normal;
        var position = contact.point + direction * 0.1f;
        var trace = Instantiate(peeTracePrefab, position, Quaternion.LookRotation(direction));
    }

    public SuppliersManager.SupplierRuntimeInfo GetSupplier()
    {
        return _supplier;
    }

    public ExplosionIfno GetExplosionIfno()
    {
        return explosionInfo;
    }
}
