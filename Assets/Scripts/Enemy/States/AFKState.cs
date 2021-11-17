using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "AFKState", menuName = "ScriptableObjects/EnemyStates/AFKState", order = 1)]
public class AFKState : EnemyState
{

    public override EnemyState UpdateLogic(GameObject gameObject)
    {
        return null;
    }

    public override void UpdatePhysics(GameObject gameObject)
    {
        Rigidbody2D rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        PatrollingAIController eac = gameObject.GetComponent<PatrollingAIController>();
        base.applyYForces(rigidbody2D, eac);
    }
}
