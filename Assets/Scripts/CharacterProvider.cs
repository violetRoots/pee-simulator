using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class CharacterProvider : MonoBehaviour
{
    public CharacterStateController StateController => stateController;
    [SerializeField] private CharacterStateController stateController;

    public CharacterMovementController MovementController => movementController;
    [SerializeField] private CharacterMovementController movementController;

    public CharacterInteractionController InteractionController => interactionController;
    [SerializeField] private CharacterInteractionController interactionController;

    public CharacterBuildController BuildController => buildController;
    [SerializeField] private CharacterBuildController buildController;

    public CharacterDamageController DamageController => damageController;
    [SerializeField] private CharacterDamageController damageController;

#if UNITY_EDITOR
    private void OnValidate()
    {
        stateController = GetComponent<CharacterStateController>();
        movementController = GetComponent<CharacterMovementController>();
        interactionController = GetComponent<CharacterInteractionController>();
        buildController = GetComponent<CharacterBuildController>();
        damageController = GetComponent<CharacterDamageController>();
    }
#endif
}
