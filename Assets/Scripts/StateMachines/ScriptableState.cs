using JetBrains.Annotations;
using UnityEngine;

public abstract class ScriptableState<T> : ScriptableObject, IBaseState<T>
    where T : class, IBaseState<T>
{
    public virtual void Enter(GameObject gameObject) {
        
    }

    public virtual T UpdateLogic(GameObject gameObject) {
        return null;
    }

    public virtual void UpdatePhysics(GameObject gameObject) {
        
    }

    public virtual void Exit(GameObject gameObject) {
        
    }
}