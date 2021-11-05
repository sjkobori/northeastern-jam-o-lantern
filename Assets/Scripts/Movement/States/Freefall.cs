using UnityEngine;

[CreateAssetMenu(fileName = "FreefallState", menuName = "ScriptableObjects/MovementStates/Freefall", order = 1)]
public class Freefall: AXMoveState {
    [SerializeField] private MovementState idleState;
    [SerializeField] private MovementState movingState;
    [SerializeField] private MovementState wallSlidingState;
    private bool _preserveXMoveSpeed;

    public override void Enter(GameObject gameObject)
    {
        base.Enter(gameObject);
        // gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        gameObject.GetComponentInChildren<Animator>().SetBool("Grounded", false);
        
        _preserveXMoveSpeed = true;
    }


    public override MovementState UpdateLogic(GameObject gameObject)
    {
        PlayerMovementController pmc = gameObject.GetComponent<PlayerMovementController>();
        pmc.jump.Consume();
        if (Mathf.Abs(pmc.horizontalAxis) > Mathf.Epsilon)
        {
            _preserveXMoveSpeed = false;
        }
        if (pmc.grounded) {
            //transition to idle state if input = 0
            if (Mathf.Abs(pmc.horizontalAxis) < Mathf.Epsilon)
            {
                return idleState;
            }

            return movingState;
        }

        if (pmc.walled)
        {
            
            return wallSlidingState;
        }

        return null;
    }

    protected override float getXMoveSpeed(PlayerMovementController pmc, Rigidbody2D rigidbody)
    {
        if (_preserveXMoveSpeed)
        {
            return rigidbody.velocity.x;
        }else
        {
            return base.getXMoveSpeed(pmc, rigidbody);
        }
       
    }


}