using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeeBox : MonoBehaviour
{
    public PeeGenerator Generator { get; private set; }

    [SerializeField] private int scoreAddiction = 10;

    [SerializeField] private Transform peeTracePrefab;

    private GameManager _gameManager;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _gameManager = GameManager.Instance;

        _rigidbody = GetComponent<Rigidbody>();
    }

    public void SetGenerator(PeeGenerator generator)
    {
        Generator = generator;
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

        _gameManager.Data.SetScore(_gameManager.Data.Score + scoreAddiction);
    }
}
