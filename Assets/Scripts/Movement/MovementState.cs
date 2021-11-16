using UnityEngine;

public abstract class MovementState : ScriptableState<MovementState> {

    public MovementState knockbackState;
    public override void UpdatePhysics(GameObject gameObject) {
        Rigidbody2D rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        PlayerMovementController pmc = gameObject.GetComponent<PlayerMovementController>();

        applyGravity(rigidbody2D, pmc);
        capVerticalSpeed(rigidbody2D, pmc);
        
        
        
        base.UpdatePhysics(gameObject);

    }

    public override MovementState UpdateLogic(GameObject gameObject)
    {
        PlayerController pc = gameObject.GetComponent<PlayerController>();

        if (pc.takingDamage)
        {
            Debug.Log("Getting knocked back");
            return knockbackState;
        }

        return base.UpdateLogic(gameObject);
    }


    protected virtual void applyGravity(Rigidbody2D rigidbody, PlayerMovementController pmc)
    {
        rigidbody.AddForce(Vector2.down * pmc.gravity.value);
    }

    private void capVerticalSpeed(Rigidbody2D rigidbody, PlayerMovementController pmc)
    {
        if (Mathf.Abs(rigidbody.velocity.y) > getVerticalCap(pmc))
        {
            float directionY = Mathf.Sign(rigidbody.velocity.y);
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, getVerticalCap(pmc) * directionY);
        }
    }

    protected virtual float getVerticalCap(PlayerMovementController pmc)
    {

        return pmc.maxVerticalSpeed.value;
    }

    
}