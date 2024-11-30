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

    public void SetContext(SuppliersManager.SupplierRuntimeInfo peeSupplierRuntimeInfo)
    {
        //preferIcon.sprite = peeSupplierRuntimeInfo.configData.iconSprite;

        gameObject.SetActive(true);
    }
}
