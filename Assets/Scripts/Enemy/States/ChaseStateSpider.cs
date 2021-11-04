using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ChaseStateSpider", menuName = "ScriptableObjects/EnemyStates/ChaseStateSpider", order = 1)]
public class ChaseStateSpider : EnemyState
{

    [SerializeField]
    private EnemyState climbWallState;
    [SerializeField]
    private EnemyState jumpState;
    public override EnemyState UpdateLogic(GameObject gameObject)
    {

        EnemyAIController ec = gameObject.GetComponent<EnemyAIController>();
        base.UpdateLogic(gameObject);
        if (ec.wallSideRight || ec.wallSideLeft)
        {
            return climbWallState; 
        } else if ((ec.bottomRightEmpty && ec.transform.localScale.x < 0) || (ec.bottomLeftEmpty && ec.transform.localScale.x > 0))
        {
            return jumpState;
        }

        return null;
    }

    public override void UpdatePhysics(GameObject gameObject)
    {
        EnemyAIController eac = gameObject.GetComponent<EnemyAIController>();

        float newX;
        float playerX = eac.playerPos.transform.position.x;
        float enemyX = eac.transform.position.x;
        if (enemyX  < playerX - 6 || (enemyX > playerX && enemyX < playerX + 5))
        {
            newX = enemyX + eac.stats.chaseSpeed * Time.deltaTime;
        } else if (enemyX > playerX + 6 || (enemyX < playerX && enemyX > playerX - 5))
        {
            newX = enemyX - eac.stats.chaseSpeed * Time.deltaTime;
        } else
        {
            newX = enemyX;
        }
        if (playerX > enemyX)
        {
            eac.transform.localScale = new Vector2(Mathf.Abs(eac.transform.localScale.x), eac.transform.localScale.y);
        } else
        {
            eac.transform.localScale = new Vector2(-Mathf.Abs(eac.transform.localScale.x), eac.transform.localScale.y);
        }

        eac.transform.position = new Vector2(newX, eac.transform.position.y);

        base.UpdatePhysics(gameObject);
        
    }

}
