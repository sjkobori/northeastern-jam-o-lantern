
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
        PlayerMovementController pmc = gameObject.GetComponent<PlayerMovementController>();
        _jumpTime += Time.deltaTime;
        if ((!pmc.jumpHeld && _jumpTime > minJumpTime) || _jumpTime >= maxJumpTime) {
            return freefallState;
        }

        return null;
    }

    protected override void applyGravity(Rigidbody2D rigidbody, PlayerMovementController pmc)
    {
        rigidbody.AddForce(Vector2.down * pmc.jumpGravity.value);
    }
}