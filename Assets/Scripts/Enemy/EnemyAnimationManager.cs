

public class EnemyAnimationManager : AnimationManager
{
    //dies
    public void die()
    {
        animator.Play("Dead");
    }


}

