
using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(fileName = "JumpingState", menuName = "ScriptableObjects/MovementStates/Jumping", order = 1)]
public class Jumping : AXMoveState {
    [SerializeField] private MovementState freefallState;

    //private float _jumpStart;
    private float _jumpTime;
    [SerializeField]
    private float maxJumpTime;
    [SerializeField]
    private float minJumpTime;


    public override void Enter(GameObject gameObject)
    {
        PlayerMovementController pmc = gameObject.GetComponent<PlayerMovementController>();
        _jumpTime = 0;
        // gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;

        Rigidbody2D rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, pmc.jumpSpeed.value);
        gameObject.GetComponentInChildren<Animator>().SetTrigger("Jump");
        gameObject.GetComponentInChildren<Animator>().SetBool("Grounded", false);
        base.Enter(gameObject);
    }

    [CanBeNull]
    public override MovementState UpdateLogic(GameObject gameObject) {
        base.UpdateLogic(gameObject);
        PlayerMovementController pmc = gameObject.GetComponent<PlayerMovementController>();
        _jumpTime += Time.deltaTime;
        if ((!pmc.jumpHeld && _jumpTime > minJumpTime) || _jumpTime >= maxJumpTime) {
            return freefallState;
        }

        return base.UpdateLogic(gameObject);
    }

    protected override void applyGravity(Rigidbody2D rigidbody, PlayerMovementController pmc)
    {
        rigidbody.AddForce(Vector2.down * pmc.jumpGravity.value);
    }
}