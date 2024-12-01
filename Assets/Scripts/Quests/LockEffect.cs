using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockEffect : MonoBehaviour
{
    [SerializeField] private bool isActive;

    [Space]
    [MinMaxSlider(0, 2)]
    [SerializeField] private Vector2 scaleBounds;
    [SerializeField] private float speed = 1.0f;

    private float _lerpValue;

    private void Update()
    {
        if (!isActive) return;

        _lerpValue = Mathf.PingPong(speed * Time.unscaledTime, 1.0f);
        var scaleValue = Mathf.Lerp(scaleBounds.x, scaleBounds.y, _lerpValue);
        transform.localScale = new Vector2(scaleValue, scaleValue);
    }

    public void SetIsActive(bool isActive)
    {
        this.isActive = isActive;
    }
}
