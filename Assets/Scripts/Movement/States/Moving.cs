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
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
    }

    [CanBeNull]
    public override MovementState UpdateLogic(GameObject gameObject)
    {
        PlayerMovementController pmc = gameObject.GetComponent<PlayerMovementController>();
        Rigidbody2D rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        //transition to idle state if input = 0
        if (pmc.jump) {
            return jumpingState;
        }

        if (!pmc.grounded) {
            return freefallState;
        }
        if (Mathf.Abs(rigidbody2D.velocity.x) < Mathf.Epsilon)
        {
            return idleState;
        }

        return base.UpdateLogic(gameObject); 
    }


}
