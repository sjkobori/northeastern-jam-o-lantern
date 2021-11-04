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

        EnemyAIController ec = gameObject.GetComponent<EnemyAIController>();

        base.UpdateLogic(gameObject);
        if (Vector2.Distance((Vector2) ec.playerPos.position, (Vector2) ec.transform.position) < 1)
        {
            ec.GetComponent<BoxCollider2D>().isTrigger = true;
            return suckState;
        }

        return null;
    }

    public override void UpdatePhysics(GameObject gameObject)
    {
        EnemyAIController eac = gameObject.GetComponent<EnemyAIController>();

        float x = this.moveRLTowards(eac.playerPos.position, eac.transform.position, gameObject, eac.stats.chaseSpeed);
        float y = this.moveUDTowards(eac.playerPos.position, eac.transform.position, gameObject, eac.stats.chaseSpeed/2, (float) -0.05);
        eac.transform.position = new Vector2(x, y);
        base.UpdatePhysics(gameObject);
    }

}
