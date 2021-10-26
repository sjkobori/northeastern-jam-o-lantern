using UnityEngine;

public abstract class MovementState : ABaseState<MovementState> {
    protected FloatReference speed;
    
    public MovementState(GameObject gameObject, FloatReference speed): base(gameObject) {
        this.speed = speed;
    }

    public override MovementState UpdateLogic() {
        return null;
    }

    public override void UpdatePhysics() {
        
    } 

    public override void Enter() {
    }

    public override void Exit() {
        
    }
}