using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine<T> : MonoBehaviour where T : IBaseState<T> {
    T currentState;

    // Start is called before the first frame update
    void Start()
    {
        currentState = GetInitialState();
        if (currentState != null)
        {
            currentState.Enter();
        }
    }

    protected abstract T GetInitialState();

    // Update is called once per frame
    void Update()
    {
        if (currentState != null)
        {
            T newState = currentState.UpdateLogic();
            if (newState != null) {
                ChangeState(newState);
            }
        }
    }

    void FixedUpdate()
    {
        if (currentState != null)
        {
            currentState.UpdatePhysics();
        }
    }

    public void ChangeState(T newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();

    }

    private void OnGUI()
    {
        string content = currentState == null ? "NO STATE ASSIGNED" : currentState.GetType().Name;
        GUILayout.Label($"<color='black'><size=40>{content}</size></color>");
    }

}
