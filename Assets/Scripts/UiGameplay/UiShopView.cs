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
    [SerializeField] private UiShopUpperButton roomsButton;
    [SerializeField] private Button exitButton;

    [Header("Panels")]
    [SerializeField] private GameObject suppliersPanel;
    [SerializeField] private GameObject questsPanel;
    [SerializeField] private GameObject checksPanel;
    [SerializeField] private GameObject roomsPanel;

    [Header("Rooms")]
    [SerializeField] private UiRoomCell roomCell;
    [SerializeField] RectTransform roomsContentObj;

    private GameManager _gameManager;
    private RoomsManager _roomsManager;

    private void Awake()
    {
        _gameManager = GameManager.Instance;
        _roomsManager = _gameManager.RoomsManager;

        InitRooms();

        suppliersButton.Subscribe(ShowSuppliers);
        questsButton.Subscribe(ShowQuests);
        checksButton.Subscribe(ShowChecks);
        roomsButton.Subscribe(ShowRooms);

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

    private void InitRooms()
    {
        var rooms = _roomsManager.GetRooms();
        foreach (var room in rooms)
        {
            var cell = Instantiate(roomCell, roomsContentObj);
            cell.SetContext(room);
        }
    }

    private void ShowSuppliers()
    {
        suppliersPanel.SetActive(true);
        questsPanel.SetActive(false);
        checksPanel.SetActive(false);
        roomsPanel.SetActive(false);

        suppliersButton.IsActive = true;
        questsButton.IsActive = false;
        checksButton.IsActive = false;
        roomsButton.IsActive = false;
    }

    private void ShowQuests()
    {
        suppliersPanel.SetActive(false);
        questsPanel.SetActive(true);
        checksPanel.SetActive(false);
        roomsPanel.SetActive(false);

        suppliersButton.IsActive = false;
        questsButton.IsActive = true;
        checksButton.IsActive = false;
        roomsButton.IsActive = false;
    }

    private void ShowChecks()
    {
        suppliersPanel.SetActive(false);
        questsPanel.SetActive(false);
        checksPanel.SetActive(true);
        roomsPanel.SetActive(false);

        suppliersButton.IsActive = false;
        questsButton.IsActive = false;
        checksButton.IsActive = true;
        roomsButton.IsActive = false;
    }

    private void ShowRooms()
    {
        suppliersPanel.SetActive(false);
        questsPanel.SetActive(false);
        checksPanel.SetActive(false);
        roomsPanel.SetActive(true);

        suppliersButton.IsActive = false;
        questsButton.IsActive = false;
        checksButton.IsActive = false;
        roomsButton.IsActive = true;
    }

    private void OnExitButton()
    {
        gameObject.SetActive(false);
    }
}
