using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(fileName = "MovingState", menuName = "ScriptableObjects/MovementStates/Moving", order = 2)]
public class Moving : MovementState
{
    private float _horizontalInput;
    private Rigidbody2D _rigidbody2D;
    public FloatReference speed;
    public MovementState idleState;
    
    public override void Enter(GameObject gameObject)
    {
        base.Enter(gameObject);
        _horizontalInput = 0f;
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
    }

    [CanBeNull]
    public override MovementState UpdateLogic(GameObject gameObject)
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        //transition to idle state if input = 0
        if (Mathf.Abs(_horizontalInput) < Mathf.Epsilon)
        {
            return idleState;
        }

        return base.UpdateLogic(gameObject); 
    }

    public override void UpdatePhysics(GameObject gameObject)
    {
        base.UpdatePhysics(gameObject);
        Vector2 vel = _rigidbody2D.velocity;
        vel.x = _horizontalInput * speed.value;
        _rigidbody2D.velocity = vel;
    }
}
