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

    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material peeMaterial;

    [SerializeField] private Renderer[] renderers;

    [SerializeField]
    [HideInInspector]
    private ZombieProvider _zombieProvider;
    private ZombieStateController _zombieStateController;

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
        _zombieStateController = _zombieProvider.StateController;
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

        _HP -= peeBoxDamage;
        Destroy(peeBox.gameObject);

        if(_HP <= 0)
        {
            _zombieStateController.state.Value = ZombieStateController.ZombieState.Die;
        }
    }

    private void UpdatePeeEffect()
    {
        if (Time.time - _lastPeeTime < peeEffectDuration)
            UpdateMaterials(peeMaterial);
        else
            UpdateMaterials(defaultMaterial);
    }

    private void UpdateMaterials(Material material)
    {
        foreach (var renderer in renderers)
        {
            renderer.material = material;
        }
    }
}
