using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PatrollerState", menuName = "ScriptableObjects/EnemyStates/PatrollerState", order = 1)]
public class PatrollerState : EnemyState
{

    [SerializeField]
    private EnemyState chaseState;

    public override void Enter(GameObject gameObject) {
        var animator = gameObject.GetComponentInChildren<Animator>();
        animator.SetBool("Moving", true);
        animator.SetFloat("MoveSpeed", 1);
        base.Enter(gameObject);
    }

    [HideInInspector]
    private bool finishedRight = false;
    public override EnemyState UpdateLogic(GameObject gameObject)
    {

        PatrollingAIController ec = gameObject.GetComponent<PatrollingAIController>();

        
        if (ec.inAggro)
        {
            return chaseState;
        }

        return base.UpdateLogic(gameObject);
    }

    public override void UpdatePhysics(GameObject gameObject)
    {
        Rigidbody2D rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        PatrollingAIController eac = gameObject.GetComponent<PatrollingAIController>();
        /*
        if (Vector2.Distance((Vector2)eac.transform.position, eac.destination) < 1)
        {
            eac.setNextPos();
        }
        */
        eac.transform.position = new Vector2(moveRLTowards(eac.destination, eac.transform.position, gameObject, eac.stats.moveSpeed), eac.transform.position.y);
        //base.applyYForces(rigidbody2D, eac);
        base.UpdatePhysics(gameObject);
    }

}