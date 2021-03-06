using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IdleState", menuName = "ScriptableObjects/MovementStates/Idle", order = 1)]
public class Idle : AXMoveState
{
    //private float _horizontalInput;
    [SerializeField] private MovementState movingState;
    [SerializeField] private MovementState jumpingState;
    [SerializeField] private MovementState freefallState;

    public override void Enter(GameObject gameObject) {
        gameObject.GetComponentInChildren<Animator>().SetBool("Grounded", true);
    }

    public override MovementState UpdateLogic(GameObject gameObject)
    {
        PlayerMovementController pmc = gameObject.GetComponent<PlayerMovementController>();

        //transition to idle state if input = 0
        if (pmc.jump.Consume()) {
            return jumpingState;
        }

        if (!pmc.grounded) {
            return freefallState;
        }
        //transition to moving state if input !=0
        if (Mathf.Abs(pmc.horizontalAxis) > Mathf.Epsilon)
        {
            return movingState;
        }

        return base.UpdateLogic(gameObject);
    }
}
