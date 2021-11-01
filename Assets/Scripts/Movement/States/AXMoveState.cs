using UnityEngine;

public abstract class AXMoveState: MovementState {
    public override void UpdatePhysics(GameObject gameObject)
    {
        Rigidbody2D rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        PlayerMovementController pmc = gameObject.GetComponent<PlayerMovementController>();

        if (Mathf.Abs(pmc.horizontalAxis) > Mathf.Epsilon) {
            pmc.Face(Vector2.right * Mathf.Sign(pmc.horizontalAxis));
        }
        rigidbody2D.velocity = new Vector2(getXMoveSpeed(pmc, rigidbody2D), rigidbody2D.velocity.y);

        gameObject.GetComponentInChildren<Animator>().SetFloat("MoveSpeed", Mathf.Abs(pmc.horizontalAxis));

        base.UpdatePhysics(gameObject);
    }

    protected virtual float getXMoveSpeed(PlayerMovementController pmc, Rigidbody2D rigidbody)
    {
        return pmc.horizontalAxis * pmc.groundMoveSpeed.value;
    }
}