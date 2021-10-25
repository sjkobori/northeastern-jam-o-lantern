using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : BaseState
{
    private MovementSM _sm;
    private float _horizontalInput;

    public Idle(MovementSM stateMachine) : base("Idle", stateMachine) {
        _sm = (MovementSM)stateMachine;
    }
   public override void Enter()
    {
        base.Enter();
        _horizontalInput = 0f;
        _sm.spriteRenderer.color = Color.black;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        _horizontalInput = Input.GetAxis("Horizontal");
        //transition to moving state if input !=0
        if (Mathf.Abs(_horizontalInput) > Mathf.Epsilon)
        {
            stateMachine.ChangeState(_sm.movingState);
        }
    }
}
