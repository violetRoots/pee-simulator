using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UiSuppliersPanel : MonoBehaviour
{
    [SerializeField] private UiSupplierCell peeSupplierCell;
    [SerializeField] private RectTransform suppliersContentObj;

    private SuppliersManager _suppliersManager;

    private List<UiSupplierCell> _supplierCells = new();

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
        var suppliers = _suppliersManager.GetSuppliers()
                                         .OrderBy(s => s.configData.price);

        foreach (var supplier in suppliers)
        {
            var cell = Instantiate(peeSupplierCell, suppliersContentObj);
            cell.SetContext(supplier);

            _supplierCells.Add(cell);
        }
    }

    private void UpdateSuppliers()
    {
        foreach (var cell in _supplierCells)
        {
            cell.gameObject.SetActive(cell.RuntimeInfo.isAvailable);
        }
    }
}
