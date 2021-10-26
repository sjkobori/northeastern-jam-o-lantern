using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IdleState", menuName = "ScriptableObjects/MovementStates/Idle", order = 1)]
public class Idle : MovementState
{
    private float _horizontalInput;
    public MovementState movingState;

   public override void Enter(GameObject gameObject)
    {
        base.Enter(gameObject);
        _horizontalInput = 0f;
        gameObject.GetComponent<SpriteRenderer>().color = Color.black;
    }

    public override MovementState UpdateLogic(GameObject gameObject)
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        //transition to moving state if input !=0
        if (Mathf.Abs(_horizontalInput) > Mathf.Epsilon)
        {
            return movingState;
        }

        return base.UpdateLogic(gameObject);
    }
}
