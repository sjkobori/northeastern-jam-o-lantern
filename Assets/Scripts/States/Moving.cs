using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Moving : MovementState
{
    private float _horizontalInput;
    private Rigidbody2D _rigidbody2D;

    public Moving(GameObject gameObject, FloatReference speed) : base(gameObject, speed) {
        
    }
    
   public override void Enter()
    {
        base.Enter();
        _horizontalInput = 0f;
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
    }

    [CanBeNull]
    public override MovementState UpdateLogic()
    {
        base.UpdateLogic();
        _horizontalInput = Input.GetAxis("Horizontal");
        //transition to idle state if input = 0
        if (Mathf.Abs(_horizontalInput) < Mathf.Epsilon)
        {
            return new Idle(gameObject, speed);
        }

        return base.UpdateLogic(); 
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        Vector2 vel = _rigidbody2D.velocity;
        vel.x = _horizontalInput * speed.value;
        _rigidbody2D.velocity = vel;
    }
}
