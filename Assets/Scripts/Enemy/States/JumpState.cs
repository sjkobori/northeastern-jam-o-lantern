using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "JumpState", menuName = "ScriptableObjects/EnemyStates/JumpState", order = 1)]
public class JumpState : EnemyState {

    [SerializeField]
    private EnemyState chaseStateSpider;
    [SerializeField]
    private EnemyState climbWallState;
    public override void Enter(GameObject gameObject)
    {
        EnemyAIController ec = gameObject.GetComponent<EnemyAIController>();
        Rigidbody2D rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        rigidbody2D.velocity = new Vector2(ec.stats.chaseSpeed * Mathf.Sign(ec.transform.localScale.x), 10);
    }
    public override EnemyState UpdateLogic(GameObject gameObject)
    {
        
        EnemyAIController ec = gameObject.GetComponent<EnemyAIController>();

        base.UpdateLogic(gameObject);
        if (ec.grounded)
        {
            return chaseStateSpider;
        } else if (ec.wallSideLeft || ec.wallSideRight)
        {
            return climbWallState;
        }

        return null;
    }
}
