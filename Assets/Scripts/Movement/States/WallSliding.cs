using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(fileName = "WallSlidingState", menuName = "ScriptableObjects/MovementStates/WallSliding", order = 1)]
public class WallSliding : AXMoveState
{
    [SerializeField] private MovementState idleState;
    [SerializeField] private MovementState movingState;
    [SerializeField] private MovementState freefallState;
    [SerializeField] private MovementState walljumpState;
    public override void Enter(GameObject gameObject)
    {
        base.Enter(gameObject);
        gameObject.GetComponent<SpriteRenderer>().color = Color.green;
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
        if (pmc.jump)
        {
            return walljumpState;
        }

        

        return null;
    }
}