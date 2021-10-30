using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "JumpState", menuName = "ScriptableObjects/EnemyStates/JumpState", order = 1)]
public class JumpState : EnemyState {

    [SerializeField]
    private EnemyState jumpState;
    public override void Enter(GameObject gameObject)
    {
        Rigidbody2D rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 10);
    }
    public override EnemyState UpdateLogic(GameObject gameObject)
    {
        
        EnemyAIController ec = gameObject.GetComponent<EnemyAIController>();

        base.UpdateLogic(gameObject);
        if (ec.grounded)
        {
            return jumpState;
        }

        return null;
    }
}
