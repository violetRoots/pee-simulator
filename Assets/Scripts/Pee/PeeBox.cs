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

    public enum PeeType
    {
        Box = 0,
        Bubble = 1,
        Ray = 2
    }

    public PeeType Type => type;
    [SerializeField] private PeeType type;
    [SerializeField] private ExplosionIfno explosionInfo;
    [SerializeField] private int scoreAddiction = 10;

    [SerializeField] private Transform peeTracePrefab;

    private Rigidbody _rigidbody;
    private SupplierRuntimeInfo _supplier;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void PeeForward(Vector3 direction, float stregnth, SupplierRuntimeInfo supplier = null)
    {
        _supplier = supplier;

        _rigidbody.AddForce(direction * stregnth);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (type == PeeType.Ray) return;

        Destroy(gameObject);

        var contact = collision.contacts[0];
        var direction = contact.normal;
        var position = contact.point + direction * 0.1f;
        var trace = Instantiate(peeTracePrefab, position, Quaternion.LookRotation(direction));
    }

    public SupplierRuntimeInfo GetSupplier()
    {
        return _supplier;
    }

    public ExplosionIfno GetExplosionIfno()
    {
        return explosionInfo;
    }

    public void SetDestroyTimer(float delay)
    {
        Destroy(gameObject, delay);
    }

    public float GetTypeMultiplier()
    {
        if (type == PeeType.Box)
            return 1.0f;
        else if (type == PeeType.Bubble)
            return 2.0f;
        else if (type == PeeType.Ray)
            return 250.0f;
        else return 1.0f;
    }
}
