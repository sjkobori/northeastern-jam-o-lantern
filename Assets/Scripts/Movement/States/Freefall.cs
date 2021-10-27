using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(fileName = "FreefallState", menuName = "ScriptableObjects/MovementStates/Freefall", order = 1)]
public class Freefall: AXMoveState {
    [SerializeField] private MovementState idleState;
    [SerializeField] private MovementState movingState;

    // [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundedLayers;
    
    [CanBeNull]
    public override MovementState UpdateLogic(GameObject gameObject)
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        var groundTest = Physics2D.OverlapBoxAll(new Vector3(0, -0.5f) + gameObject.transform.position, new Vector2(1, 0.1f), 0, groundedLayers).Length > 0;

        if (groundTest) {
            //transition to idle state if input = 0
            if (Mathf.Abs(_horizontalInput) < Mathf.Epsilon)
            {
                return idleState;
            }

            return movingState;
        }

        return null;
    }
}