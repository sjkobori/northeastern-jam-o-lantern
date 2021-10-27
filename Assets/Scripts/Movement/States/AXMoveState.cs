using UnityEngine;

public abstract class AXMoveState: MovementState {
    public override void UpdatePhysics(GameObject gameObject)
    {
        PlayerMovementController pmc = gameObject.GetComponent<PlayerMovementController>();
        targetVel.x = pmc.horizontalAxis * pmc.moveSpeed;
        base.UpdatePhysics(gameObject);
    }
}