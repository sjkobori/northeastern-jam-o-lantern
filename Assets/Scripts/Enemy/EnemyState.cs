using UnityEngine;

public abstract class EnemyState : ScriptableState<EnemyState> {
    protected Vector2 targetVel;
    private Vector2 _velocity;
    private float xMaxSpeed = 400;


    protected EnemyState() {
        _velocity = new Vector2();
    }
    
    public override void UpdatePhysics(GameObject gameObject) {
        Rigidbody2D rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        EnemyAIController pmc = gameObject.GetComponent<EnemyAIController>();

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

    private void applyForces(Rigidbody2D rigidbody, EnemyAIController pmc)
    {
        applyYForces(rigidbody, pmc);
    }

    protected virtual void applyYForces(Rigidbody2D rigidbody, EnemyAIController pmc)
    {
        rigidbody.AddForce(Vector2.down * pmc.gravity.value);
    }
}