using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingAIController : EnemyAIController
{
    public int aggroRadius;
    public Transform[] patrolAreas;
    public LayerMask player;
   // [HideInInspector]
   // public Transform playerPos;
    [HideInInspector]
    public bool inAggro;

    private int numPatrolAreas;
    [HideInInspector]
    public Vector2 destination;
    private int destIndex;
    private List<Vector2> destPoints;

    private Collider2D[] hitColliders;

    private HitboxController _aggroRange;
    [HideInInspector]
    public Vector2 playerCenter;

    protected override void Awake()
    {
        _aggroRange = GetComponentInChildren<HitboxController>();
        destPoints = new List<Vector2>();
        foreach (Transform t in patrolAreas)
        {
            destPoints.Add(t.position);
        }
        destIndex = 0;
        destination = destPoints[destIndex];
        
        numPatrolAreas = patrolAreas.Length;
        inAggro = false;
        base.Awake();
    }

    private void CheckPlayer()
    {
        if (_aggroRange.colliding )
        {
            inAggro = true;
            playerCenter = _aggroRange.collisionCenter;
        } else
        {
            inAggro = false;
        }
    }

    protected override void Update()
    {
        /*
        var hitColliders = Physics2D.OverlapCircleAll(transform.position, aggroRadius, player);
        if (hitColliders.Length > 0)
        {
            playerPos = hitColliders[0].transform;
            inAggro = true;
        }
        */
        if (Mathf.Abs((transform.position.x - destination.x)) < Mathf.Epsilon)
        {

            setNextPos();
        }
        CheckPlayer();
        base.Update();
    }

    public void setNextPos()
    {
       
        destIndex =  (destIndex + 1) % numPatrolAreas;
        destination = destPoints[destIndex];
    }
}
