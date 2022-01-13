using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "RollerPaceState", menuName = "ScriptableObjects/EnemyStates/RollerPaceState", order = 1)]
public class RollerPaceState : EnemyState
{

    private bool atLegdeWall;
    private Vector2 direction;
    [SerializeField]
    private EnemyState deathState;

    public override void Enter(GameObject gameObject) {
        var animationManager = gameObject.GetComponentInChildren<RollerAnimationManager>();
        animationManager.setMoveSpeed(1);
        animationManager.move();
        atLegdeWall = false;
        direction = Vector2.right;
        base.Enter(gameObject);
    }

    public override EnemyState UpdateLogic(GameObject gameObject)
    {

        RollerAIController ec = gameObject.GetComponent<RollerAIController>();
        Debug.Log("inside update logic" + ec.dying);


        if (ec.dying)
        {
            Debug.Log("Going to death state");
            return deathState;
        }
        return base.UpdateLogic(gameObject);
    }

    public override void UpdatePhysics(GameObject gameObject)
    {

        RollerAIController eac = gameObject.GetComponent<RollerAIController>();
        

        eac.transform.position = new Vector2(moveRLTowards((Vector2)eac.transform.position + direction, eac.transform.position, gameObject, eac.stats.moveSpeed), eac.transform.position.y);
        if (atLegdeWall)
        {
            eac.Face(-direction);
        }
        //base.applyYForces(rigidbody2D, eac);
        base.UpdatePhysics(gameObject);
    }

}