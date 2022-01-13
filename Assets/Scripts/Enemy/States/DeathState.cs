using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "DeathState", menuName = "ScriptableObjects/EnemyStates/DeathState", order = 1)]
public class DeathState : EnemyState
{


    public override void Enter(GameObject gameObject) {
        var animationManager = gameObject.GetComponentInChildren<EnemyAnimationManager>();
        animationManager.die();
        Destroy(gameObject, 1.5f);
        base.Enter(gameObject);
    }

}