using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using static SuppliersManager;

public class UiShopView : MonoBehaviour
{

    [Header("Buttons")]
    [SerializeField] private UiShopUpperButton suppliersButton;
    [SerializeField] private UiShopUpperButton questsButton;
    [SerializeField] private UiShopUpperButton checksButton;
    [SerializeField] private UiShopUpperButton suppliersShopButton;
    [SerializeField] private Button exitButton;

    [Header("Panels")]
    [SerializeField] private GameObject suppliersPanel;
    [SerializeField] private GameObject questsPanel;
    [SerializeField] private GameObject checksPanel;
    [SerializeField] private GameObject suppliersShopPanel;

    private GameManager _gameManager;

    private void Awake()
    {
        _gameManager = GameManager.Instance;

        InitSuppliersShop();

        suppliersButton.Subscribe(ShowSuppliers);
        questsButton.Subscribe(ShowQuests);
        checksButton.Subscribe(ShowChecks);
        suppliersShopButton.Subscribe(ShowSuppliersShop);

        exitButton.onClick.AddListener(OnExitButton);
    }

    private void OnEnable()
    {
        _gameManager.AddLock(this);

        ShowSuppliers();
    }

    private void OnDisable()
    {
        _gameManager.RemoveLock(this);
    }

    private void InitSuppliersShop()
    {
        
    }

    private void ShowSuppliers()
    {
        suppliersPanel.SetActive(true);
        questsPanel.SetActive(false);
        checksPanel.SetActive(false);
        suppliersShopPanel.SetActive(false);

        suppliersButton.IsActive = true;
        questsButton.IsActive = false;
        checksButton.IsActive = false;
        suppliersShopButton.IsActive = false;
    }

    private void ShowQuests()
    {
        suppliersPanel.SetActive(false);
        questsPanel.SetActive(true);
        checksPanel.SetActive(false);
        suppliersShopPanel.SetActive(false);

        suppliersButton.IsActive = false;
        questsButton.IsActive = true;
        checksButton.IsActive = false;
        suppliersShopButton.IsActive = false;
    }

    private void ShowChecks()
    {
        suppliersPanel.SetActive(false);
        questsPanel.SetActive(false);
        checksPanel.SetActive(true);
        suppliersShopPanel.SetActive(false);

        suppliersButton.IsActive = false;
        questsButton.IsActive = false;
        checksButton.IsActive = true;
        suppliersShopButton.IsActive = false;
    }

    private void ShowSuppliersShop()
    {
        suppliersPanel.SetActive(false);
        questsPanel.SetActive(false);
        checksPanel.SetActive(false);
        suppliersShopPanel.SetActive(true);

        suppliersButton.IsActive = false;
        questsButton.IsActive = false;
        checksButton.IsActive = false;
        suppliersShopButton.IsActive = true;
    }

    private void OnExitButton()
    {
        gameObject.SetActive(false);
    }
}
