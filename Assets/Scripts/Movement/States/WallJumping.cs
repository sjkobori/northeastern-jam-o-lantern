
using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(fileName = "WallJumpingState", menuName = "ScriptableObjects/MovementStates/WallJumping", order = 1)]
public class WallJumping : MovementState
{
    [SerializeField] private MovementState freefallState;

    private float _jumpStart;
    private float maxJumpHold = 0.15f;

    public override void Enter(GameObject gameObject)
    {
        base.Enter(gameObject);
        _jumpStart = Time.time;
        gameObject.GetComponent<SpriteRenderer>().color = Color.cyan;
    }

    [CanBeNull]
    public override MovementState UpdateLogic(GameObject gameObject)
    {
        PlayerMovementController pmc = gameObject.GetComponent<PlayerMovementController>();
        if (!pmc.jump || Time.time - _jumpStart >= maxJumpHold)
        {
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