using UnityEngine;

public abstract class AXMoveState: MovementState {
    protected float _horizontalInput;

    [SerializeField] protected FloatReference speed;

    public override void UpdatePhysics(GameObject gameObject)
    {
        targetVel.x = _horizontalInput * speed.value;
        base.UpdatePhysics(gameObject);
    }
}