using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HumanPreferPanel : MonoBehaviour
{
    [SerializeField] private Image preferIcon;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void SetContext(SuppliersManager.PeeSupplierRuntimeInfo peeSupplierRuntimeInfo)
    {
        preferIcon.sprite = peeSupplierRuntimeInfo.config.iconSprite;

        gameObject.SetActive(true);
    }
}
