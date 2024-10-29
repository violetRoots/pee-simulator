using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameManager : Singleton<GameManager>
{
    public GameplayDataContainer Data {  get; private set; } = new GameplayDataContainer();

    public CharacterProvider CharacterProvider => characterProvider;
    [SerializeField] private CharacterProvider characterProvider;

    public KassaManager KassaManager => kassaManager;
    [SerializeField] private KassaManager kassaManager;

    public SitPlaceManager SitPlaceManager => sitPlaceManager;
    [SerializeField] private SitPlaceManager sitPlaceManager;

    public ZombiePointsManager ZombiePointsManager => zombiePointsManager;
    [SerializeField] private ZombiePointsManager zombiePointsManager;

    public ExitPointsManager ExitPointsManager => exitPointsManager;
    [SerializeField] private ExitPointsManager exitPointsManager;

    private void Awake()
    {
        sitPlaceManager.Init();
    }
}
