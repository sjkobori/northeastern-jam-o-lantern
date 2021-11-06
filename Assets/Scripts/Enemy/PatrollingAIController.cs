using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingAIController : EnemyAIController
{
    public int aggroRadius;
    public Transform[] patrolAreas;
    public LayerMask player;
    [HideInInspector]
    public Transform playerPos;
    [HideInInspector]
    public bool inAggro;

    private int numPatrolAreas;
    [HideInInspector]
    public Vector2 destination;
    private int destIndex;
    private List<Vector2> destPoints;

    private Collider2D[] hitColliders;

    protected override void Awake()
    {
        destPoints = new List<Vector2>();
        foreach (Transform t in patrolAreas)
        {
            destPoints.Add(t.position);
        }
        destination = destPoints[0];
        destIndex = 0;
        numPatrolAreas = patrolAreas.Length;
        inAggro = false;
        base.Awake();
    }

    protected override void Update()
    {

        var hitColliders = Physics2D.OverlapCircleAll(transform.position, aggroRadius, player);
        if (hitColliders.Length > 0)
        {
            playerPos = hitColliders[0].transform;
            inAggro = true;
        }
        base.Update();
    }

    public void setNextPos()
    {
        if (numPatrolAreas <= destIndex + 1)
        {
            destination = destPoints[0];
            destIndex = 0;
        }else
        {
            destination = destPoints[destIndex + 1];
            destIndex += 1;
        }
    }
}
