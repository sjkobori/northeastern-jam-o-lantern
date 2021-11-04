using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ChaseStateFlyer", menuName = "ScriptableObjects/EnemyStates/ChaseStateFlyer", order = 1)]
public class ChaseStateFlyer : EnemyState
{

    [SerializeField]
    private EnemyState patrolState;

    public override EnemyState UpdateLogic(GameObject gameObject)
    {

        EnemyAIController ec = gameObject.GetComponent<EnemyAIController>();

        base.UpdateLogic(gameObject);
        if (!ec.inAggro)
        {
            return patrolState;
        }

        return null;
    }

    public override void UpdatePhysics(GameObject gameObject)
    {
        EnemyAIController eac = gameObject.GetComponent<EnemyAIController>();

        float x = this.moveRLTowards(eac.playerPos.position, eac.transform.position, gameObject, eac.stats.chaseSpeed);
        float y = this.moveUDTowards(eac.playerPos.position, eac.transform.position, gameObject, eac.stats.chaseSpeed, 4);
        eac.transform.position = new Vector2(x, y);
        base.UpdatePhysics(gameObject);


        // targetVel = new Vector2(0, rigidbody2D.velocity.y);
    }

}
