using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour , IStateManager
{
    private Dictionary<string, BaseState> states;
    private BaseState currentState;
    private BaseState lastState;

    #region States
    [SerializeField] private AttackState attackState;
    [SerializeField] private RoamState roamState;
    [SerializeField] private ChaseState chaseState;
    [SerializeField] private IdleState idleState;
    [SerializeField] private KnockBackState knockBackState;
    [SerializeField] private CastSpellState castSpellState;
    #endregion
    
    
    private void Awake()
    {
        states = new Dictionary<string, BaseState>();
        attackState.SetStateManager(this);
        roamState.SetStateManager(this);
        chaseState.SetStateManager(this);
        idleState.SetStateManager(this);
        knockBackState.SetStateManager(this);
        states.Add("Attack", attackState);
        states.Add("Idle",idleState);
        states.Add("Chase",chaseState);
        states.Add("Roam",roamState);
        states.Add("KnockBack",knockBackState);
        if (castSpellState is not null)
        {
            castSpellState.SetStateManager(this);
            states.Add("Cast",castSpellState);
        }
        currentState = roamState;
        currentState.onEnter();
        
        // special states
    }
    
    private void Update()
    {
        currentState.onUpdate();
    }

    public void SwitchState(BaseState newState)
    {
        currentState.onExit();
        SetLastState(currentState);
        currentState = newState;
        currentState.onEnter();
    }

    public BaseState GetLastState()
    {
        return lastState;
    }

    public void SetLastState(BaseState s)
    {
        lastState = s;
    }

    public void DisableStateManager()
    {
        enabled = false;
    }

    public BaseState GetState(string stateName)
    {
        if (states.TryGetValue(stateName,out BaseState value))
        {
            return value;
        }
        Debug.LogError("State Name is Wrong");
        return null;
    }
    
    
}
