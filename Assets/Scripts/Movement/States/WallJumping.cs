
using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(fileName = "WallJumpingState", menuName = "ScriptableObjects/MovementStates/WallJumping", order = 1)]
public class WallJumping : MovementState
{
    [SerializeField] private MovementState freefallState;

    //private float _jumpStart;
    //private float maxJumpHold = 0.15f;
    private float _jumpTime;
    [SerializeField]
    private float maxJumpTime;
    [SerializeField]
    private float minJumpTime;

    public override void Enter(GameObject gameObject)
    {
        _jumpTime = 0;
        // gameObject.GetComponent<SpriteRenderer>().color = Color.cyan;
        Rigidbody2D rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        PlayerMovementController pmc = gameObject.GetComponent<PlayerMovementController>();
        int direction = pmc.wallSideLeft ? 1 : -1;
        float jumpX = direction * pmc.groundMoveSpeed.value;
        
        rigidbody2D.velocity = new Vector2( jumpX,  pmc.jumpSpeed.value);

        pmc.Face(new Vector2(direction, 0));
        
        gameObject.GetComponentInChildren<Animator>().SetTrigger("WallJump");
        gameObject.GetComponentInChildren<Animator>().SetBool("Grounded", false);
        base.Enter(gameObject);
    }

    [CanBeNull]
    public override MovementState UpdateLogic(GameObject gameObject)
    {
        PlayerMovementController pmc = gameObject.GetComponent<PlayerMovementController>();
        pmc.horizontalAxis = 0;
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