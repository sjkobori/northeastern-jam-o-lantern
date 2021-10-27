using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(fileName = "FreefallState", menuName = "ScriptableObjects/MovementStates/Freefall", order = 1)]
public class Freefall: AXMoveState {
    [SerializeField] private MovementState idleState;
    [SerializeField] private MovementState movingState;
    [SerializeField] private MovementState wallSlidingState;

    public override void Enter(GameObject gameObject)
    {
        base.Enter(gameObject);
        gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
    }

    [CanBeNull]
    public override MovementState UpdateLogic(GameObject gameObject)
    {
        PlayerMovementController pmc = gameObject.GetComponent<PlayerMovementController>();

        if (pmc.grounded) {
            //transition to idle state if input = 0
            if (Mathf.Abs(pmc.horizontalAxis) < Mathf.Epsilon)
            {
                return idleState;
            }

            return movingState;
        }

        if (pmc.walled)
        {
            
            return wallSlidingState;
        }

        return null;
    }
}