using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[CreateAssetMenu(fileName = "ChaseStateRoller", menuName = "ScriptableObjects/EnemyStates/ChaseStateRoller", order = 1)]
public class ChaseStateRoller : EnemyState
{

    [SerializeField]
    private EnemyState patrolState;

    public override void Enter(GameObject gameObject) {
        var animator = gameObject.GetComponentInChildren<Animator>();
        PatrollingAIController ec = gameObject.GetComponent<PatrollingAIController>();
        animator.SetBool("Moving", true);
        animator.SetFloat("MoveSpeed", ec.stats.chaseSpeed / ec.stats.moveSpeed);
        base.Enter(gameObject);
    }

    public override EnemyState UpdateLogic(GameObject gameObject)
    {

        PatrollingAIController ec = gameObject.GetComponent<PatrollingAIController>();

       
        if (!ec.inAggro)
        {
            return patrolState;
        }

        return base.UpdateLogic(gameObject);
    }

    public override void UpdatePhysics(GameObject gameObject)
    {
        Rigidbody2D rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        PatrollingAIController eac = gameObject.GetComponent<PatrollingAIController>();

        float x = moveRLTowards(eac.playerCenter, eac.transform.position, gameObject, eac.stats.chaseSpeed);
        eac.transform.position = new Vector2(x, eac.transform.position.y);

       // base.applyYForces(rigidbody2D, eac);

        base.UpdatePhysics(gameObject);
    }

}
