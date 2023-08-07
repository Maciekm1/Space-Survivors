using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const string IS_MOVING = "IsMoving";
    private const string TURNING_LEFT = "TurningLeft";
    private const string TURNING_RIGHT = "TurningRight";

    [SerializeField] private PlayerController playerController;
    [SerializeField] private Animator engineAnimator;

    void Update()
    {
        engineAnimator.SetBool(IS_MOVING, playerController.IsMoving());
        engineAnimator.SetBool(TURNING_RIGHT, playerController.IsTurningLeft());
        engineAnimator.SetBool(TURNING_LEFT, playerController.IsTurningRight());
    }
}
