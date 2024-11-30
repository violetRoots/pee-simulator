using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeePhysicsObject : MonoBehaviour
{
    [SerializeField] private int hitPoints = 15;

    [SerializeField] private Rigidbody rb;

    private int _currentHP;

#if UNITY_EDITOR
    private void OnValidate()
    {
        rb = GetComponent<Rigidbody>();
    }
#endif

    private void Awake()
    {
        _currentHP = hitPoints;

        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out PeeBox peeBox)) return;

        _currentHP = Mathf.Clamp(_currentHP - 1, 0, hitPoints);

        if (_currentHP > 0) return;

        Debug.Log("BOOM");

        var explsionInfo = peeBox.GetExplosionIfno();

        rb.constraints = RigidbodyConstraints.None;
        rb.AddExplosionForce(explsionInfo.force, peeBox.transform.position, explsionInfo.radius);
    }
}
