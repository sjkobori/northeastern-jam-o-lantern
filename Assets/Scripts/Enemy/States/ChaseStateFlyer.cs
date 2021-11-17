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
        PatrollingAIController eac =  gameObject.GetComponent<PatrollingAIController>();

        float x = this.moveRLTowards(eac.playerCenter, eac.transform.position, gameObject, eac.stats.chaseSpeed);
        float y = this.moveUDTowards(eac.playerCenter, eac.transform.position, gameObject, eac.stats.chaseSpeed);
        eac.transform.position = new Vector2(x, y);
        base.UpdatePhysics(gameObject);
    }

}
