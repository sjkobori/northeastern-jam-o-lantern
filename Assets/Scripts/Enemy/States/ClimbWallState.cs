using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ClimbWallState", menuName = "ScriptableObjects/EnemyStates/ClimbWallState", order = 1)]
public class ClimbWallState : EnemyState
{

    [SerializeField]
    private EnemyState chaseStateSpider;

    public override EnemyState UpdateLogic(GameObject gameObject)
    {

        EnemyAIController ec = gameObject.GetComponent<EnemyAIController>();

        base.UpdateLogic(gameObject);

        if (!ec.wallSideRight && !ec.wallSideLeft)
        {
            
            return chaseStateSpider;
        }

        return null;
    }

    public override void UpdatePhysics(GameObject gameObject)
    {
        EnemyAIController eac = gameObject.GetComponent<EnemyAIController>();

        float y = eac.transform.position.y + eac.stats.chaseSpeed * Time.deltaTime;
        eac.transform.position = new Vector2(eac.transform.position.x, y);
    }

}
