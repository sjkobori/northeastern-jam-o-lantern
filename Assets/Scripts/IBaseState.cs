using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBaseState<out T> where T : IBaseState<T> {
    void Enter();

    T UpdateLogic();
    
    void UpdatePhysics();
    
    void Exit();

}
