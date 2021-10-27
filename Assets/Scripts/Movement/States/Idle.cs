using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IdleState", menuName = "ScriptableObjects/MovementStates/Idle", order = 1)]
public class Idle : MovementState
{
    private float _horizontalInput;
    [SerializeField] private MovementState movingState;
    [SerializeField] private MovementState jumpingState;
    [SerializeField] private MovementState freefallState;
    
    [SerializeField] private LayerMask groundedLayers;

   public override void Enter(GameObject gameObject)
    {
        base.Enter(gameObject);
        _horizontalInput = 0f;
        gameObject.GetComponent<SpriteRenderer>().color = Color.black;
    }

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
        //transition to moving state if input !=0
        if (Mathf.Abs(_horizontalInput) > Mathf.Epsilon)
        {
            return movingState;
        }

        return base.UpdateLogic(gameObject);
    }
}
