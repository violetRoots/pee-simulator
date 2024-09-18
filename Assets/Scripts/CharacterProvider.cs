using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class CharacterProvider : MonoBehaviour
{
    [SerializeField] private CharacterInteractionController characterInteractionController;
    [SerializeField] private CharacterBuildController characterBuildController;

#if UNITY_EDITOR
    private void OnValidate()
    {
        characterInteractionController = GetComponent<CharacterInteractionController>();
        characterBuildController = GetComponent<CharacterBuildController>();
    }
#endif

    public CharacterInteractionController CharacterInteractionController
    {
        get => characterInteractionController;
    }

    public CharacterBuildController CharacterBuildController
    {
        get => characterBuildController;
    }
}
