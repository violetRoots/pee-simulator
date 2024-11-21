using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameManager : SingletonMonoBehaviourBase<GameManager>
{
    public GameplayDataContainer Data {  get; private set; } = new GameplayDataContainer();

    public GameplaySector[] GameplaySectors => gameplaySectors;
    [SerializeField] GameplaySector[] gameplaySectors;

    public CharacterProvider CharacterProvider => characterProvider;
    [SerializeField] private CharacterProvider characterProvider;

    public KassaManager KassaManager => kassaManager;
    [SerializeField] private KassaManager kassaManager;

    public SitPlaceManager SitPlaceManager => sitPlaceManager;
    [SerializeField] private SitPlaceManager sitPlaceManager;

    public ZombiePointsManager ZombiePointsManager => zombiePointsManager;
    [SerializeField] private ZombiePointsManager zombiePointsManager;

    public DoorsManager DoorsManager => doorsManager;
    [SerializeField] private DoorsManager doorsManager;

    public SuppliersManager SuppliersManager => suppliersManager;
    [SerializeField] private SuppliersManager suppliersManager;

    public QuestsManager QuestManager => questManager;
    [SerializeField] private QuestsManager questManager;

    public ChecksManager ChecksManager => checksManager;
    [SerializeField] private ChecksManager checksManager;

    public RoomsManager RoomsManager => roomsManager;
    [SerializeField] private RoomsManager roomsManager;

    public BottleManager BottleManager => bottleManager;
    [SerializeField] private BottleManager bottleManager;

    private void Awake()
    {
        InitStateSubscription();

        sitPlaceManager.Init();
        doorsManager.Init();
        suppliersManager.Init();
        questManager.Init();
        checksManager.Init();
        bottleManager.Init();

        Data.Init();
    }

    private void OnDestroy()
    {
        checksManager.Dispose();

        Data.Dispose();

        DisposeStateSubscription();
    }
}
