using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ChaseStateLeech", menuName = "ScriptableObjects/EnemyStates/ChaseStateLeech", order = 1)]
public class ChaseStateLeech : EnemyState
{

    [SerializeField]
    private EnemyState suckState;

    public override EnemyState UpdateLogic(GameObject gameObject)
    {

        PatrollingAIController ec = gameObject.GetComponent<PatrollingAIController>();

        base.UpdateLogic(gameObject);
        if (Vector2.Distance((Vector2) ec.playerPos.position, (Vector2) ec.transform.position) < 1.2f)
        {
            ec.GetComponent<BoxCollider2D>().isTrigger = true;
            return suckState;
        }

        return null;
    }

    public override void UpdatePhysics(GameObject gameObject)
    {
        PatrollingAIController eac = gameObject.GetComponent<PatrollingAIController>();

        float x = this.moveRLTowards(eac.playerPos.position, eac.transform.position, gameObject, eac.stats.chaseSpeed);
        float y = this.moveUDTowards(eac.playerPos.position, eac.transform.position, gameObject, eac.stats.chaseSpeed/2);
        eac.transform.position = new Vector2(x, y);
        base.UpdatePhysics(gameObject);
    }

}
