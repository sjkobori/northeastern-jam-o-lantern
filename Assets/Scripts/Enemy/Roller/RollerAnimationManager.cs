using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollerAnimationManager : EnemyAnimationManager
{
    //moves
    public void move()
    {
        animator.Play("Roller Moving");
    }

    public void setMoveSpeed(float speed)
    {
        animator.SetFloat("MoveSpeed", speed);
    }

    //takes dmg
    public void hurt()
    {

    }

    

    public void idle()
    {
        defaultAnimation();
    }


}
