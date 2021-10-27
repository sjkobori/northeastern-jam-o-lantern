
using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(fileName = "JumpingState", menuName = "ScriptableObjects/MovementStates/Jumping", order = 1)]
public class Jumping : AXMoveState {
    [SerializeField] private MovementState freefallState;

    private float _jumpStart;
    private float maxJumpHold = 0.15f;
   
    public override void Enter(GameObject gameObject)
    {
        base.Enter(gameObject);
        _jumpStart = Time.time;
        gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
    }

    [CanBeNull]
    public override MovementState UpdateLogic(GameObject gameObject) {
        PlayerMovementController pmc = gameObject.GetComponent<PlayerMovementController>();
        if (!pmc.jump || Time.time - _jumpStart >= maxJumpHold) {
            return freefallState;
        }

        return null;
    }
    
    public override void UpdatePhysics(GameObject gameObject) {
        targetVel.y = 10;
        base.UpdatePhysics(gameObject);
    }
}