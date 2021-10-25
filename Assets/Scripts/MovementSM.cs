using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSM : StateMachine
{
    [HideInInspector]
    public Idle idleState;
    [HideInInspector]
    public Moving movingState;

    public Rigidbody2D rigidbody2D;
    public SpriteRenderer spriteRenderer;

    public float speed = 4f;

    private void Awake()
    {
        idleState = new Idle(this);
        movingState = new Moving(this);
    }
    protected override BaseState GetInitialState()
    {
        return idleState;
    }
}
