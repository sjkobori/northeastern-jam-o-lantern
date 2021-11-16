using UnityEngine;

[CreateAssetMenu(fileName = "KnockbackState", menuName = "ScriptableObjects/MovementStates/Knockback", order = 1)]
public class Knockback : AXMoveState {
    [SerializeField] private MovementState freefallState;
    private float _knockbackTime;
    [SerializeField]
    private float maxKnockbackTime;

    
    public override void Enter(GameObject gameObject)
    {
        base.Enter(gameObject);
        PlayerController pc = gameObject.GetComponent<PlayerController>();
        PlayerMovementController pmc = gameObject.GetComponent<PlayerMovementController>();
        
        _knockbackTime = 0;
        Rigidbody2D rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        Vector2 ec = (rigidbody2D.position - pc.enemyCenter).normalized;
        rigidbody2D.velocity = new Vector2(ec.x,ec.y ) * pmc.knockbackSpeed.value;
        //rigidbody2D.velocity = new Vector2(0, 500);
        gameObject.GetComponentInChildren<Animator>().SetBool("Grounded", false);

    }

    public override void UpdatePhysics(GameObject gameObject)
    {
        Rigidbody2D rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        PlayerMovementController pmc = gameObject.GetComponent<PlayerMovementController>();

        applyGravity(rigidbody2D, pmc);

    }


    public override MovementState UpdateLogic(GameObject gameObject)
    {
        PlayerMovementController pmc = gameObject.GetComponent<PlayerMovementController>();

        _knockbackTime += Time.deltaTime;
        if (_knockbackTime > maxKnockbackTime)
        {
            return freefallState;
        }

        return null;
    }

    protected override float getXMoveSpeed(PlayerMovementController pmc, Rigidbody2D rigidbody)
    {
        return rigidbody.velocity.x;
    }


}