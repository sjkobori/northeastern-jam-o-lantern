using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ChaseStateRoller", menuName = "ScriptableObjects/EnemyStates/ChaseStateRoller", order = 1)]
public class ChaseStateRoller : EnemyState
{

    [SerializeField]
    private EnemyState patrolState;

    public override EnemyState UpdateLogic(GameObject gameObject)
    {

        PatrollingAIController ec = gameObject.GetComponent<PatrollingAIController>();

        base.UpdateLogic(gameObject);
        if (!ec.inAggro)
        {
            return patrolState;
        }

        return null;
    }

    public override void UpdatePhysics(GameObject gameObject)
    {
        Rigidbody2D rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        PatrollingAIController eac = gameObject.GetComponent<PatrollingAIController>();

        float x = this.moveRLTowards(eac.playerPos.position, eac.transform.position, gameObject, eac.stats.chaseSpeed);
        eac.transform.position = new Vector2(x, eac.transform.position.y);

        base.applyYForces(rigidbody2D, eac);

        base.UpdatePhysics(gameObject);
    }

}
