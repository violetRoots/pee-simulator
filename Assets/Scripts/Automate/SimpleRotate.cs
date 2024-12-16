using UnityEngine;

public class SimpleRotate : BasePeePart
{
    [SerializeField] private float speed = 100.0f;

    [SerializeField] private Transform target;

    private Space _space;
    private Vector3 _upAxis;

    private void Awake()
    {
        _upAxis = Vector3.up;
        _space = Space.Self;
    }

    private void Update()
    {
        if (!IsActive) return;

        target.Rotate(_upAxis, speed * Time.deltaTime, _space);
    }
}
