using UnityEngine;

public abstract class MovementState : ScriptableState<MovementState> {
    protected Vector2 targetVel;
    private Vector2 _velocity;
    private float xMaxSpeed = 400;


    protected MovementState() {
        targetVel = new Vector2();
        _velocity = new Vector2();
    }
    
    public override void UpdatePhysics(GameObject gameObject) {
        Rigidbody2D rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        PlayerMovementController pmc = gameObject.GetComponent<PlayerMovementController>();

        applyForces(rigidbody2D, pmc);
        /*
            rigidbody2D.velocity = new Vector2(
            Mathf.SmoothDamp(rigidbody2D.velocity.x, targetVel.x, ref _velocity.x, 0.05f, xMaxSpeed),
            Mathf.SmoothDamp(rigidbody2D.velocity.y, targetVel.y, ref _velocity.y, 0.05f, 500)
        );
        */
        base.UpdatePhysics(gameObject);

       // targetVel = new Vector2(0, rigidbody2D.velocity.y);
    }

    private void applyForces(Rigidbody2D rigidbody, PlayerMovementController pmc)
    {
        applyXForces(rigidbody, pmc);
        applyYForces(rigidbody, pmc);
    }

    protected virtual void applyXForces(Rigidbody2D rigidbody, PlayerMovementController pmc)
    {
        applyFriction(rigidbody, pmc.airFriction.value);
    }

    protected virtual void applyYForces(Rigidbody2D rigidbody, PlayerMovementController pmc)
    {
        rigidbody.AddForce(Vector2.down * pmc.gravity.value);
    }

    protected void applyFriction(Rigidbody2D rigidbody, float frictionValue)
    {
        if (rigidbody.velocity.x > Mathf.Epsilon)
        {
            rigidbody.AddForce(Vector2.left * frictionValue);
        }
        else if (rigidbody.velocity.x < Mathf.Epsilon)
        {
            rigidbody.AddForce(Vector2.right * frictionValue);
        }
    }
}