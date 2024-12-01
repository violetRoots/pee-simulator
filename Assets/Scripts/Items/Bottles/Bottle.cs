using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFx.Outline;

public class Bottle : Item
{
    public SupplierRuntimeInfo RuntimeInfo { get; private set; }

    public float FillAmount => Mathf.Clamp01(_currentFillCount / _allFillCount);
    public bool IsEmpty => Mathf.Approximately(FillAmount, 0.0f);

    [Range(0f, 1f)]
    [SerializeField] private float gorloAmount = 0.1f;
    [Range(0f, 1f)]
    [SerializeField] private float conusAmount = 0.2f;

    [SerializeField] private MeshRenderer[] stickers;
    [SerializeField] private Transform peeCyllinder;
    [SerializeField] private Transform peeConus;
    [SerializeField] private Transform peeGorlo;

    private float _currentFillCount;
    private float _allFillCount;

    //#if UNITY_EDITOR
    //    private void OnValidate()
    //    {
    //        UpdateFillVisual(fillAmount);
    //    }
    //#endif

    private void Update()
    {
        UpdateFillVisual(FillAmount);
    }

    protected override void OnItemAttack(Collider collider)
    {
        base.OnItemAttack(collider);

        if (!collider.TryGetComponent(out ZombieProvider zombieProvider)) return;

        zombieProvider.ExplosionController.Exlode(collider.ClosestPoint(transform.position));
    }

    public void SetContext(SupplierRuntimeInfo info)
    {
        RuntimeInfo = info;

        _allFillCount = _currentFillCount = info.configData.saturation;

        UpdateVisual(info);
    }

    public void PeeTick(float peeTickValue)
    {
        _currentFillCount -= peeTickValue;
    }

    private void UpdateVisual(SupplierRuntimeInfo info)
    {
        foreach (var sticker in stickers)
        {
            var material = sticker.material;
            material.SetTexture("_MainTex", info.VisualInfo.icon.texture);
            sticker.sharedMaterial = material;
        }
    }

    private void UpdateFillVisual(float amount)
    {
        peeGorlo.gameObject.SetActive(amount >= gorloAmount);
        peeConus.gameObject.SetActive(amount >= conusAmount);
        
        var newScale = peeCyllinder.localScale;
        newScale.z = Mathf.Clamp01(amount / 1.0f - conusAmount) * 100.0f;
        peeCyllinder.localScale = newScale;
    }
}
