using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]

public class MovementSM : StateMachine<MovementState>
{

    public FloatReference speed;

    protected override MovementState GetInitialState()
    {
        return new Idle(gameObject, speed);
    }
}
