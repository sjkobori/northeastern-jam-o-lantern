using UnityEngine;
[CreateAssetMenu(fileName = "SuckState", menuName = "ScriptableObjects/EnemyStates/SuckState", order = 1)]
public class SuckState : EnemyState
{
    public override EnemyState UpdateLogic(GameObject gameObject)
    {

        PatrollingAIController ec = gameObject.GetComponent<PatrollingAIController>();

        if (ec.wallSideLeft || ec.wallSideRight)
        {
            Destroy(gameObject);
        }

        return null;
    }

    public override void UpdatePhysics(GameObject gameObject)
    {
        PatrollingAIController eac = gameObject.GetComponent<PatrollingAIController>();
        eac.transform.position = new Vector2(eac.playerPos.position.x, eac.playerPos.position.y);
        base.UpdatePhysics(gameObject);


    }

}
