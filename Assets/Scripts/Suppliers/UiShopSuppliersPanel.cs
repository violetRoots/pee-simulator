using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiShopSuppliersPanel : MonoBehaviour
{
    [SerializeField] private UiShopSuppliersCell peeShopSupplierCell;
    [SerializeField] private RectTransform shopSuppliersContentObj;

    private SuppliersManager _suppliersManager;

    private List<UiShopSuppliersCell> _shopSupplierCells = new();

    private void Awake()
    {
        _suppliersManager = GameManager.Instance.SuppliersManager;

        _suppliersManager.onUpdateSupplierVisability += UpdateSuppliers;

        InitSuppliers();
        UpdateSuppliers();
    }

    private void OnDestroy()
    {
        _suppliersManager.onUpdateSupplierVisability -= UpdateSuppliers;
    }

    private void InitSuppliers()
    {
        var suppliers = _suppliersManager.GetShopSuppliers();

        foreach (var supplier in suppliers)
        {
            var cell = Instantiate(peeShopSupplierCell, shopSuppliersContentObj);
            cell.SetContext(supplier);

            _shopSupplierCells.Add(cell);
        }
    }

    private void UpdateSuppliers()
    {
        foreach (var cell in _shopSupplierCells)
        {
            cell.gameObject.SetActive(!cell.RuntimeInfo.isAvailable);
        }
    }
}
