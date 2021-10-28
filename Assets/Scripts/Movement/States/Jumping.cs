
using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(fileName = "JumpingState", menuName = "ScriptableObjects/MovementStates/Jumping", order = 1)]
public class Jumping : AXMoveState {
    [SerializeField] private MovementState freefallState;

    //private float _jumpStart;
    private float _jumpTime;
    private float maxJumpTime = 0.75f;
    private float minJumpTime = 0.2f;

    public override void Enter(GameObject gameObject)
    {
        base.Enter(gameObject);
        _jumpTime = 0;
        gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;

        Rigidbody2D rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, rigidbody2D.velocity.y + 10);
    }

    [CanBeNull]
    public override MovementState UpdateLogic(GameObject gameObject) {
        PlayerMovementController pmc = gameObject.GetComponent<PlayerMovementController>();
        _jumpTime += Time.deltaTime;
        Debug.Log("jumptime is:" + _jumpTime);
        if ((!pmc.jump && _jumpTime > minJumpTime) || _jumpTime >= maxJumpTime) {
            Debug.Log("Going to freefall, reache max hold time:" + maxJumpTime);
            return freefallState;
        }

        return null;
    }
    
    public override void UpdatePhysics(GameObject gameObject) {
        PlayerMovementController pmc = gameObject.GetComponent<PlayerMovementController>();
 
        //targetVel.y = 10;
        base.UpdatePhysics(gameObject);
    }

    protected override void applyYForces(Rigidbody2D rigidbody, PlayerMovementController pmc)
    {
        rigidbody.AddForce(Vector2.down * pmc.jumpGravity.value);
    }
}