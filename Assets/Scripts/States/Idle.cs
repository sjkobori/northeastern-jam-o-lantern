using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : MovementState
{
    private float _horizontalInput;

    public Idle(GameObject gameObject, FloatReference speed) : base(gameObject, speed) {
        
    }
   public override void Enter()
    {
        base.Enter();
        _horizontalInput = 0f;
        gameObject.GetComponent<SpriteRenderer>().color = Color.black;
    }

    public override MovementState UpdateLogic()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        //transition to moving state if input !=0
        if (Mathf.Abs(_horizontalInput) > Mathf.Epsilon)
        {
            return new Moving(gameObject, speed);
        }

        return base.UpdateLogic();
    }
}
