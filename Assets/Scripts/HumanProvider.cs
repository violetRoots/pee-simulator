using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanProvider : MonoBehaviour
{
    public HumanStateController StateController => stateController;
    [SerializeField] private HumanStateController stateController;

    public HumanMovementController MovementController => movementController;
    [SerializeField] private HumanMovementController movementController;

    public HumanContentController ContentController => contentController;
    [SerializeField] private HumanContentController contentController;

    public HumanAnimationController AnimationController => animationController;
    [SerializeField] private HumanAnimationController animationController;

    public HumanRunAwayChecker RunAwayChecker => runAwayChecker;
    [SerializeField] private HumanRunAwayChecker runAwayChecker;

    public HumanPeeController PeeController => peeController;
    [SerializeField] private HumanPeeController peeController;

    public HumanEmotionController EmotionController => emotionController;
    [SerializeField] private HumanEmotionController emotionController;
}
