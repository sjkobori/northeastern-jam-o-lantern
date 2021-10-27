using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(fileName = "MovingState", menuName = "ScriptableObjects/MovementStates/Moving", order = 2)]
public class Moving : AXMoveState
{
    [SerializeField] protected MovementState idleState;
    [SerializeField] private MovementState jumpingState;
    [SerializeField] private MovementState freefallState;
    
    [SerializeField] private LayerMask groundedLayers;
    
    public override void Enter(GameObject gameObject)
    {
        _horizontalInput = 0f;
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
    }

    [CanBeNull]
    public override MovementState UpdateLogic(GameObject gameObject)
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        bool _jumping = Input.GetButton("Jump");
        
        var groundTest = Physics2D.OverlapBoxAll(new Vector3(0, -0.5f) + gameObject.transform.position, new Vector2(1, 0.1f), 0, groundedLayers).Length > 0;

        //transition to idle state if input = 0
        if (_jumping) {
            return jumpingState;
        }

        if (!groundTest) {
            return freefallState;
        }
        if (Mathf.Abs(_horizontalInput) < Mathf.Epsilon)
        {
            return idleState;
        }

        return base.UpdateLogic(gameObject); 
    }
}
