using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(fileName = "MovingState", menuName = "ScriptableObjects/MovementStates/Moving", order = 2)]
public class Moving : AXMoveState
{
    [SerializeField] protected MovementState idleState;
    [SerializeField] private MovementState jumpingState;
    [SerializeField] private MovementState freefallState;
    
    public override void Enter(GameObject gameObject)
    {
        // gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        gameObject.GetComponentInChildren<Animator>().SetBool("Grounded", true);
        gameObject.GetComponentInChildren<Animator>().SetBool("Moving", true);
        base.Enter(gameObject);
    }

    public override void Exit(GameObject gameObject) {
        gameObject.GetComponentInChildren<Animator>().SetBool("Moving", false);
        base.Exit(gameObject);
    }

    [CanBeNull]
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
        if (Mathf.Abs(pmc.horizontalAxis) < Mathf.Epsilon)
        {
            return idleState;
        }

        return base.UpdateLogic(gameObject); 
    }


}
