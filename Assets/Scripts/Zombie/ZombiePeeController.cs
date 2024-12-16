using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static HumanPeeController;

public class ZombiePeeController : MonoBehaviour
{
    [SerializeField] private int peeBoxDamage = 10;
    [SerializeField] private int startHP = 100;
    [SerializeField] private float peeEffectDuration = 1.0f;

    [SerializeField] private Color defaultColorMaterial;
    [SerializeField] private Color peeColorMaterial;

    [SerializeField] private Renderer[] renderers;

    [SerializeField]
    [HideInInspector]
    private ZombieProvider _zombieProvider;
    private ZombieExplosionController _explosionController;

#if UNITY_EDITOR
    private void OnValidate()
    {
        _zombieProvider = GetComponent<ZombieProvider>();
    }
#endif

    private float _lastPeeTime;

    private int _HP;

    private void Awake()
    {
        _explosionController = _zombieProvider.ExplosionController;
    }

    private void Start()
    {
        _HP = startHP;
    }

    private void Update()
    {
        UpdatePeeEffect();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (!collider.TryGetComponent(out PeeBox peeBox)) return;

        _lastPeeTime = Time.time;

        _HP -= (int) (peeBoxDamage * peeBox.GetTypeMultiplier());

        if (_HP < 0)
        {
            _explosionController.Exlode(collider.ClosestPoint(peeBox.transform.position));
        }

        if(peeBox.Type != PeeBox.PeeType.Ray)
            Destroy(peeBox.gameObject);
    }

    private void UpdatePeeEffect()
    {
        if (Time.time - _lastPeeTime < peeEffectDuration)
            UpdateMaterials(peeColorMaterial);
        else
            UpdateMaterials(defaultColorMaterial);
    }

    private void UpdateMaterials(Color colorMaterial)
    {
        foreach (var renderer in renderers)
        {
            var material = renderer.material;
            material.color = colorMaterial;
            renderer.sharedMaterial = material;
        }
    }
}
