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
        EnemyAIController eac = gameObject.GetComponent<EnemyAIController>();

        applyForces(rigidbody2D, eac);
        /*
            rigidbody2D.velocity = new Vector2(
            Mathf.SmoothDamp(rigidbody2D.velocity.x, targetVel.x, ref _velocity.x, 0.05f, xMaxSpeed),
            Mathf.SmoothDamp(rigidbody2D.velocity.y, targetVel.y, ref _velocity.y, 0.05f, 500)
        );
        */
        base.UpdatePhysics(gameObject);

       // targetVel = new Vector2(0, rigidbody2D.velocity.y);
    }

    private void applyForces(Rigidbody2D rigidbody, EnemyAIController eac)
    {
        applyYForces(rigidbody, eac);
    }

    protected virtual void applyYForces(Rigidbody2D rigidbody, EnemyAIController eac)
    {
        rigidbody.AddForce(Vector2.down * eac.gravity.value);
    }

    protected float moveRLTowards(Vector2 moveTo, Vector2 moveFrom, GameObject gameObject, float moveSpeed)
    {
        EnemyAIController eac = gameObject.GetComponent<EnemyAIController>();
        
        float remainingDist = Mathf.Abs(moveTo.x - moveFrom.x);
        //if we are not there, go there
        if (remainingDist >= Mathf.Epsilon)
        {
            float direction = Mathf.Sign(moveTo.x - moveFrom.x);
            float moveDistance = Mathf.Min(moveSpeed * Time.fixedDeltaTime, remainingDist);
            
            eac.transform.localScale = new Vector2(direction * Mathf.Abs(eac.transform.localScale.x), 
                eac.transform.localScale.y);
            return direction * moveDistance + moveFrom.x;
        }

        return moveFrom.x;

        

    }

    protected float moveUDTowards(Vector2 moveTo, Vector2 moveFrom, GameObject gameObject, float moveSpeed)
    {
        EnemyAIController eac = gameObject.GetComponent<EnemyAIController>();
        if (moveFrom.y < moveTo.y)
        {
            return moveFrom.y + moveSpeed * Time.deltaTime;
        }
        else if (moveFrom.y < moveTo.y)
        {
            return moveFrom.y;
        }
        else
        {
            return moveFrom.y - moveSpeed * Time.deltaTime;
        }
    }
}