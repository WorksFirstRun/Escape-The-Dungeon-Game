using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour , IStateManager
{
    private Dictionary<string, BaseState> states;
    private BaseState currentState;
    private BaseState lastState;

    #region States
    [SerializeField] private PlayerAttackState attackState;
    [SerializeField] private PlayerDashState dashState;
    [SerializeField] private PlayerIdleState idleState;
    [SerializeField] private PlayerMoveState moveState;
    #endregion
    
    public static PlayerStateManager Instance { get; private set; }

    private void Awake()
    {
        states = new Dictionary<string, BaseState>();
        Instance = this;
        attackState.SetStateManager(this);
        dashState.SetStateManager(this);
        idleState.SetStateManager(this);
        moveState.SetStateManager(this);
        states.Add("Attack", attackState);
        states.Add("Idle",idleState);
        states.Add("Dash",dashState);
        states.Add("Move", moveState);
        currentState = idleState;
        currentState.onEnter();
    }

    private void Update()
    {
        currentState.onUpdate();
    }

    public void SwitchState(BaseState newState)
    {
        if (enabled)
        {
            currentState.onExit(); 
            SetLastState(currentState);
            currentState = newState;
            currentState.onEnter();
        }
    }

    private void OnDestroy()
    {
        states.Clear();
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

    public void EnableStateManager()
    {
        enabled = true;
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
