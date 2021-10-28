using UnityEngine;

public abstract class AXMoveState: MovementState {
    public override void UpdatePhysics(GameObject gameObject)
    {
        Rigidbody2D rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        PlayerMovementController pmc = gameObject.GetComponent<PlayerMovementController>();
        //targetVel.x = pmc.horizontalAxis * pmc.moveSpeed;
        float directionMoving = Mathf.Sign(rigidbody2D.velocity.x);
        float directionTryingToMove = Mathf.Sign(pmc.horizontalAxis);
        if (directionMoving - directionTryingToMove < Mathf.Epsilon) //in same direction
        {
            rigidbody2D.AddForce(Vector2.right * pmc.horizontalAxis * pmc.moveSpeed);
        }
        else
        {
            rigidbody2D.AddForce(Vector2.right * pmc.horizontalAxis * pmc.moveSpeed * 4);
        }
        
        if (Mathf.Abs(rigidbody2D.velocity.x) > pmc.maxMoveSpeed.value)
        {
            rigidbody2D.velocity = new Vector2(Mathf.Sign(rigidbody2D.velocity.x) * pmc.maxMoveSpeed.value, rigidbody2D.velocity.y);
        }
        base.UpdatePhysics(gameObject);
    }
}