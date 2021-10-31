
using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(fileName = "WallJumpingState", menuName = "ScriptableObjects/MovementStates/WallJumping", order = 1)]
public class WallJumping : MovementState
{
    [SerializeField] private MovementState freefallState;

    //private float _jumpStart;
    //private float maxJumpHold = 0.15f;
    private float _jumpTime;
    private float maxJumpTime = 0.75f;
    private float minJumpTime = 0.2f;

    public override void Enter(GameObject gameObject)
    {
        base.Enter(gameObject);
        _jumpTime = 0;
        gameObject.GetComponent<SpriteRenderer>().color = Color.cyan;
        Rigidbody2D rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        PlayerMovementController pmc = gameObject.GetComponent<PlayerMovementController>();
        float jumpX = (pmc.wallSideLeft ? 1 : -1) * pmc.groundMoveSpeed.value;
        
        rigidbody2D.velocity = new Vector2( jumpX,  pmc.jumpSpeed.value);
        
    }

    [CanBeNull]
    public override MovementState UpdateLogic(GameObject gameObject)
    {
        PlayerMovementController pmc = gameObject.GetComponent<PlayerMovementController>();
        pmc.horizontalAxis = 0;
        _jumpTime += Time.deltaTime;
        Debug.Log("jumptime is:" + _jumpTime);
        if ((!pmc.jump && _jumpTime > minJumpTime) || _jumpTime >= maxJumpTime)
        {
            Debug.Log("Going to freefall, reache max hold time:" + maxJumpTime);
            return freefallState;
        }

        return null;
    }

    protected override void applyGravity(Rigidbody2D rigidbody, PlayerMovementController pmc)
    {
        rigidbody.AddForce(Vector2.down * pmc.jumpGravity.value);
    }
}