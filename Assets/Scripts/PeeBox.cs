using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeeBox : MonoBehaviour
{
    [SerializeField] private Transform peeTracePrefab;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void PeeForward(Vector3 direction, float stregnth)
    {
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
}
