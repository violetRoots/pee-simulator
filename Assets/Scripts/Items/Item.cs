using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFx.Outline;

public abstract class Item : MonoBehaviour
{

    protected Rigidbody Rigidbody => rb;

    [SerializeField] private float attackCollisionSpeed = 10.0f;

    [SerializeField] private BasicLookInteractionController lookInteraction;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Collider itemCollider;

    private CharacterItemController _characterItemController;

    private Transform _startParent;

#if UNITY_EDITOR
    private void OnValidate()
    {
        lookInteraction = GetComponentInChildren<BasicLookInteractionController>();
        rb = GetComponentInChildren<Rigidbody>();
        itemCollider = GetComponentInChildren<Collider>();

        _startParent = transform.parent;
    }
#endif

    protected virtual void Awake()
    {
        _characterItemController = GameManager.Instance.CharacterProvider.ItemController;

        lookInteraction.CustomInteration = OnItemInteract;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (Rigidbody.velocity.magnitude < attackCollisionSpeed) return;

        OnItemAttack(collider);
    }

    protected virtual void OnItemAttack(Collider collider)
    {

    }

    protected virtual void OnItemInteract()
    {
        _characterItemController.GetItem(this);
    }

    public void Attach(Transform parent)
    {
        rb.isKinematic = true;
        itemCollider.enabled = false;

        transform.SetParent(parent);
        transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
    }

    public void Detach()
    {
        rb.isKinematic = false;
        itemCollider.enabled = true;

        transform.SetParent(_startParent);
    }

    public void Throw(Vector3 direction, float force)
    {
        rb.AddForce(direction * force);
    }
}
