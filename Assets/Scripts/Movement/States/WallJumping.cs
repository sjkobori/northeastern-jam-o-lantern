
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
        int jumpX = pmc.wallSideLeft ? 10 : -10;
        rigidbody2D.velocity = new Vector2( jumpX,  10);
        
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

    public override void UpdatePhysics(GameObject gameObject)
    {
        targetVel.y = 10;
        targetVel.x = 100;
        base.UpdatePhysics(gameObject);
    }
}