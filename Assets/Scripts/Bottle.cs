using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFx.Outline;

public class Bottle : Item
{
    public PeeSupplierConfig Supplier { get; private set; }

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

    public void SetSupplier(PeeSupplierConfig supplier)
    {
        Supplier = supplier;

        _allFillCount = _currentFillCount = supplier.saturation;

        UpdateVisual(supplier);
    }

    public void PeeTick(float peeTickValue)
    {
        _currentFillCount -= peeTickValue;
    }

    private void UpdateVisual(PeeSupplierConfig supplier)
    {
        foreach (var sticker in stickers)
        {
            sticker.sharedMaterial.mainTexture = supplier.iconSprite.texture;
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
