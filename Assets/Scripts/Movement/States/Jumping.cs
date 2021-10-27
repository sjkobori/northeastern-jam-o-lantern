
using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(fileName = "JumpingState", menuName = "ScriptableObjects/MovementStates/Jumping", order = 1)]
public class Jumping : AXMoveState {
    [SerializeField] private MovementState freefallState;

    private float _jumpStart;
    private float maxJumpHold = 0.15f;
    

    public override void Enter(GameObject gameObject) {
        Debug.Log("Jumping!");
        _jumpStart = Time.time;
    }

    [CanBeNull]
    public override MovementState UpdateLogic(GameObject gameObject) {
        _horizontalInput = Input.GetAxis("Horizontal");
        bool _jumping = Input.GetButton("Jump");
        if (!_jumping || Time.time - _jumpStart >= maxJumpHold) {
            return freefallState;
        }

        return null;
    }
    
    public override void UpdatePhysics(GameObject gameObject) {
        targetVel.y = 10;
        base.UpdatePhysics(gameObject);
    }
}