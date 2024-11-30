using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameManager : SingletonMonoBehaviourBase<GameManager>
{
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

    public QuestsManager QuestsManager => questManager;
    [SerializeField] private QuestsManager questManager;

    public ChecksManager ChecksManager => checksManager;
    [SerializeField] private ChecksManager checksManager;

    public RoomsManager RoomsManager => roomsManager;
    [SerializeField] private RoomsManager roomsManager;

    public BottleManager BottleManager => bottleManager;
    [SerializeField] private BottleManager bottleManager;

    public LightManager LightManager => lightManager;
    [SerializeField] private LightManager lightManager;

    private void Awake()
    {
        SavesManager.Instance.PlayerStats.Load();

        InitStateSubscription();

        sitPlaceManager.Init();
        doorsManager.Init();
        suppliersManager.Init();
        questManager.Init();
        checksManager.Init();
        bottleManager.Init();
    }

    private void OnDestroy()
    {
        questManager.Dispose();
        checksManager.Dispose();

        DisposeStateSubscription();
    }
}
