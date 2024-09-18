using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameManager : Singleton<GameManager>
{
    public GameplayDataContainer Data {  get; private set; } = new GameplayDataContainer();

    public CharacterProvider CharacterProvider => characterProvider;

    [SerializeField] private CharacterProvider characterProvider;
}
