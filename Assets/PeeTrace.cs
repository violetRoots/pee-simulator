using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeeTrace : MonoBehaviour
{
    [SerializeField] private float lifeTime;

    [Space]
    [SerializeField] private SpriteRenderer spriteRenderer;

    private float _startlifeTime = 0.0f;

    private void Start()
    {
        _startlifeTime = Time.time;
    }

    private void Update()
    {
        var subTime = Time.time - _startlifeTime;

        if(subTime > lifeTime)
        {
            Destroy(gameObject);
            return;
        }

        var color = spriteRenderer.color;
        color.a = Mathf.Lerp(1.0f, 0.0f, subTime / lifeTime);
        spriteRenderer.color = color;
    }
}
