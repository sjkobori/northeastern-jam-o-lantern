using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(EnemyAIController))]
[RequireComponent(typeof(AnimationManager))]

public class EnemySM : StateMachine<EnemyState>
{

}
