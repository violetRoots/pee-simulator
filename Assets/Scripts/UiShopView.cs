using System.Collections;
using System.Collections.Generic;
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

    [Header("Suppliers")]
    [SerializeField] private UiPeeSupplierCell peeSupplierCell;
    [SerializeField] private RectTransform suppliersContentObj;

    [Header("Quests")]
    [SerializeField] private UiQuestCell questCell;
    [SerializeField] private RectTransform questsContentObj;

    [Header("Checks")]
    [SerializeField] private UiCheckCell checkCell;
    [SerializeField] RectTransform checksContentObj;

    [Header("Rooms")]
    [SerializeField] private UiRoomCell roomCell;
    [SerializeField] RectTransform roomsContentObj;

    private GameManager _gameManager;
    private SuppliersManager _suppliersManager;
    private QuestsManager _questsManager;
    private ChecksManager _checksManager;
    private RoomsManager _roomsManager;

    private void Awake()
    {
        _gameManager = GameManager.Instance;
        _suppliersManager = _gameManager.SuppliersManager;
        _questsManager = _gameManager.QuestManager;
        _checksManager = _gameManager.ChecksManager;
        _roomsManager = _gameManager.RoomsManager;

        InitSuppliers();
        InitQuests();
        InitChecks();
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

    private void InitSuppliers()
    {
        var suppliers = _suppliersManager.GetAvailableSuppliers();
        foreach (var supplier in suppliers)
        {
            var cell = Instantiate(peeSupplierCell, suppliersContentObj);
            cell.SetContext(supplier.config);
        }
    }

    private void InitQuests()
    {
        var quests = _questsManager.GetQuests();
        foreach (var quest in quests)
        {
            var cell = Instantiate(questCell, questsContentObj);
            cell.SetContext(quest.config);
        }
    }

    private void InitChecks()
    {
        var checks = _checksManager.GetChecks();
        foreach (var check in checks)
        {
            var cell = Instantiate(checkCell, checksContentObj);
            cell.SetContext(check.config);
        }
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
