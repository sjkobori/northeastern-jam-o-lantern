using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine<T> : MonoBehaviour where T : class, IBaseState<T> {
    T currentState;

    public T initialState;

    // Start is called before the first frame update
    void Start()
    {
        currentState = initialState;
        currentState?.Enter(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        T newState = currentState?.UpdateLogic(gameObject);
        if (newState != null) ChangeState(newState);
    }

    void FixedUpdate()
    {
        currentState?.UpdatePhysics(gameObject);
    }

    public void ChangeState(T newState)
    {
        currentState?.Exit(gameObject);
        currentState = newState;
        currentState?.Enter(gameObject);

    }

    private void OnGUI()
    {
        string content = currentState == null ? "NO STATE ASSIGNED" : currentState.GetType().Name;
        GUILayout.Label($"<color='black'><size=40>{content}</size></color>");
    }

}
