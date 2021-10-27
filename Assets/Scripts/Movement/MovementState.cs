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

        rigidbody2D.velocity = new Vector2(
            Mathf.SmoothDamp(rigidbody2D.velocity.x, targetVel.x, ref _velocity.x, 0.05f, xMaxSpeed),
            Mathf.SmoothDamp(rigidbody2D.velocity.y, targetVel.y, ref _velocity.y, 0.05f, 500)
        );
        
        base.UpdatePhysics(gameObject);

        targetVel = new Vector2(0, rigidbody2D.velocity.y);
    }
}