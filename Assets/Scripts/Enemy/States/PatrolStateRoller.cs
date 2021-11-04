using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PatrolStateRoller", menuName = "ScriptableObjects/EnemyStates/PatrolStateRoller", order = 1)]
public class PatrolStateRoller : EnemyState
{

    [SerializeField]
    private EnemyState chaseState;

    [HideInInspector]
    private bool finishedRight = false;

    public override EnemyState UpdateLogic(GameObject gameObject)
    {

        EnemyAIController ec = gameObject.GetComponent<EnemyAIController>();

        base.UpdateLogic(gameObject);
        if (ec.inAggro)
        {
            return chaseState;
        }

        return null;
    }

    public override void UpdatePhysics(GameObject gameObject)
    {
        Rigidbody2D rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        EnemyAIController eac = gameObject.GetComponent<EnemyAIController>();
        BoxCollider2D patrolArea = eac.patrolArea;
        Vector2 patrolAreaLeft = new Vector2(patrolArea.transform.position.x - patrolArea.size.x / 2 * patrolArea.transform.localScale.x, patrolArea.transform.position.y);
        Vector2 patrolAreaRight = new Vector2(patrolArea.transform.position.x + patrolArea.size.x / 2 * patrolArea.transform.localScale.x, patrolArea.transform.position.y);
        float newX;
        if (!Physics2D.IsTouching(patrolArea, eac.GetComponent<BoxCollider2D>()))
        {
            newX = this.moveRLTowards(patrolArea.transform.position, eac.transform.position, gameObject, eac.stats.moveSpeed);
        }
        else if (!finishedRight)
        {
            newX = this.moveRLTowards(patrolAreaLeft, eac.transform.position, gameObject, eac.stats.moveSpeed);
            if (Vector2.Distance((Vector2)eac.transform.position, patrolAreaLeft) < 1 )
            {
                finishedRight = true;
            }
        } else
        {
            newX = this.moveRLTowards(patrolAreaRight, eac.transform.position, gameObject, eac.stats.moveSpeed);
            if (Vector2.Distance((Vector2)eac.transform.position, patrolAreaRight) < 1)
            {
                finishedRight = false;
            }
        }
        eac.transform.position = new Vector2(newX, eac.transform.position.y);
        base.applyYForces(rigidbody2D, eac);
        base.UpdatePhysics(gameObject);


    }
}