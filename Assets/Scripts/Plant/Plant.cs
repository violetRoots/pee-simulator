using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField] private int hpCount = 100;
    [SerializeField] private float hpPeeMultiplier = 1.0f;
    [SerializeField] private float hpTimeMultiplier = 1.0f;
    
    [SerializeField] private Color greenColor;
    [SerializeField] private Color yellowColor;

    [SerializeField] private MeshRenderer meshRenderer;

    private int HP
    {
        get => _hp;
        set => _hp = Mathf.Clamp(value, 0, hpCount);
    }
    private int _hp;

    private bool _canWither = true;

    private void Start()
    {
        _hp = hpCount;

        StartCoroutine(WitherProcess());
    }

    private IEnumerator WitherProcess()
    {
        while (_canWither)
        {
            HP -= (int) (1 * hpTimeMultiplier);
            var color = Color.Lerp(yellowColor, greenColor, (float) _hp / hpCount);
            UpdateColor(color);

            yield return new WaitForSeconds(1.0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.TryGetComponent(out PeeBox peeBox)) return;

        HP += (int)(1 * hpPeeMultiplier);
        var color = Color.Lerp(yellowColor, greenColor, (float)_hp / hpCount);
        UpdateColor(color);
    }

    private void UpdateColor(Color color)
    {
        var ms = meshRenderer.materials;
        ms[1].color = color;
        meshRenderer.sharedMaterials = ms;
    }
}
