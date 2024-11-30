using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieProvider : MonoBehaviour
{
    public ZombieMovementController MovementController => movementController;
    [SerializeField] private ZombieMovementController movementController;

    public ZombieDetectionController DetectionController => detectionController;
    [SerializeField] private ZombieDetectionController detectionController;

    public ZombieStateController StateController => stateController;
    [SerializeField] private ZombieStateController stateController;

    public ZombieAnimationController AnimationController => animationController;
    [SerializeField] private ZombieAnimationController animationController;

    public ZombiePeeController PeeController => peeController;
    [SerializeField] private ZombiePeeController peeController;

    public ZombieAttackController AttackController => attackController;
    [SerializeField] private ZombieAttackController attackController;

    public ZombieExplosionController ExplosionController => explosionController;
    [SerializeField] private ZombieExplosionController explosionController;
}
