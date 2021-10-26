using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBaseState<out T> where T : IBaseState<T> {
    void Enter(GameObject gameObject);

    T UpdateLogic(GameObject gameObject);
    
    void UpdatePhysics(GameObject gameObject);
    
    void Exit(GameObject gameObject);

}
