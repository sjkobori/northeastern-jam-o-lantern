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
        PlayerMovementController pmc = gameObject.GetComponent<PlayerMovementController>();

       // float direction = Mathf.Sign(pmc.horizontalAxis);
        //Rigidbody2D rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        //rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, rigidbody2D.velocity.y + 10);
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

    protected override void applyXForces(Rigidbody2D rigidbody, PlayerMovementController pmc)
    {
        float groundFriction = pmc.groundFriction.value;
        float directionTryingToMove = Mathf.Sign(pmc.horizontalAxis);
        float directionMoving = Mathf.Sign(rigidbody.velocity.x);
        if (Mathf.Abs(pmc.horizontalAxis) < Mathf.Epsilon) //not holding stick
        {
            
        }
        else if (Mathf.Abs(directionTryingToMove - directionMoving) < Mathf.Epsilon)
        {
            groundFriction = 0;
        }
        else
        {
            groundFriction *= 2;
        }
        applyFriction(rigidbody, pmc, pmc.groundFriction.value);
    }
}
