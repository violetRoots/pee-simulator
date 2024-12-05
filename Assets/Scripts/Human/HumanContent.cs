using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanContent : MonoBehaviour
{
    public HumanType HumanType => humanType;
    [SerializeField] private HumanType humanType;

    public Collider FightCollider => fightCollider;
    [SerializeField] private Collider fightCollider;

#if UNITY_EDITOR
    private void OnValidate()
    {
        //fightCollider = GetComponentInChildren<Collider>();
    }
#endif
}

public enum HumanType
{
    None = 0,
    Kats = 1,
    Fbk = 2,
    Buisness = 3,
    GreenPeace = 4,
    Gamer = 5,
    Femka = 6,
    Elephant = 7,
    Donkey = 8
}
