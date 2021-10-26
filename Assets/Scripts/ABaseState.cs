using UnityEngine;

public abstract class ABaseState<T> : IBaseState<T>
    where T : IBaseState<T>
{
    protected GameObject gameObject;
    
    public ABaseState(GameObject gameObject) {
        this.gameObject = gameObject;
    }

    public abstract void Enter();

    public abstract void Exit();

    public abstract T UpdateLogic();

    public abstract void UpdatePhysics();
}