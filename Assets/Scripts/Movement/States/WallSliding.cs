using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(fileName = "WallSlidingState", menuName = "ScriptableObjects/MovementStates/WallSliding", order = 1)]
public class WallSliding : AXMoveState
{
    [SerializeField] private MovementState idleState;
    [SerializeField] private MovementState movingState;
    [SerializeField] private MovementState freefallState;
    [SerializeField] private MovementState walljumpState;

    public override void Enter(GameObject gameObject) {
        gameObject.GetComponentInChildren<Animator>().SetBool("OnWall", true);
    }
    
    public override void Exit(GameObject gameObject) {
        gameObject.GetComponentInChildren<Animator>().SetBool("OnWall", false);
    }

    [CanBeNull]
    public override MovementState UpdateLogic(GameObject gameObject)
    {
        PlayerMovementController pmc = gameObject.GetComponent<PlayerMovementController>();
        if (pmc.grounded)
        {
            //transition to idle state if input = 0
            if (Mathf.Abs(pmc.horizontalAxis) < Mathf.Epsilon)
            {
                return idleState;
            }

            return movingState;
        }
        if (!pmc.walled)
        {
            return freefallState;
        }
        if (pmc.jump.Consume())
        {
            return walljumpState;
        }

        

        return null;
    }

    public override void UpdatePhysics(GameObject gameObject) {
        base.UpdatePhysics(gameObject);
        
        PlayerMovementController pmc = gameObject.GetComponent<PlayerMovementController>();

        int direction = pmc.wallSideLeft ? -1 : 1;
        pmc.Face(new Vector2(direction, 0));
        // if (!pmc.wallSideLeft || !pmc.wallSideRight) {
        //     if (pmc.wallSideLeft) {
        //         pmc.Face(Vector2.right);
        //     } else if (pmc.wallSideRight) {
        //         pmc.Face(Vector2.left);
        //     }
        // }
    }

    protected override float getVerticalCap(PlayerMovementController pmc)
    {

        return pmc.wallSlideSpeedRatio.value * base.getVerticalCap(pmc);
    }
}