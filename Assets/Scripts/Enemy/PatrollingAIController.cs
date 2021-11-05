using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingAIController : EnemyAIController
{
    public Transform[] patrolAreas;

    [HideInInspector]
    public Transform playerPos;
    [HideInInspector]
    public bool inAggro;

    private int numPatrolAreas;
    [HideInInspector]
    public Vector2 destination;
    private int destIndex;
    private List<Vector2> destPoints;

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
        Debug.Log(destPoints[0].ToString() + " and " + destPoints[1].ToString());
    }

    protected void OnTriggerStay2D(Collider2D collision)
    {
        
        if(collision.gameObject.tag.Equals("Player"))
        {
            playerPos = collision.gameObject.transform;
            inAggro = true;
        }
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
