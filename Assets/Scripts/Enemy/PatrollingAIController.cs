using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingAIController : EnemyAIController
{
    public BoxCollider2D patrolArea;
    private Vector2 _pAreaPos;
    protected override void Awake()
    {
        base.Awake();
        _pAreaPos = patrolArea.transform.position;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        patrolArea.transform.position = _pAreaPos;
    }
}
