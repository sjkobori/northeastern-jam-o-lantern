using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    BaseState currentState;

    // Start is called before the first frame update
    void Start()
    {
        currentState = GetInitialState();
        if (currentState != null)
        {
            currentState.Enter();
        }
    }

    protected abstract BaseState GetInitialState();

    // Update is called once per frame
    void Update()
    {
        if (currentState != null)
        {
            currentState.UpdateLogic();
        }
    }

    void LateUpdate()
    {
        if (currentState != null)
        {
            currentState.UpdatePhysics();
        }
    }

    public void ChangeState(BaseState newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();

    }

    private void OnGUI()
    {
        string content = currentState == null ? "NO STATE ASSIGNED" : currentState.name;
        GUILayout.Label($"<color='black'><size=40>{content}</size></color>");
    }

}
