using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : BaseState
{
    private Vector2 directions;
    [SerializeField] private float attackRate; // should be the attackTime of the attack + the cooldown
    [SerializeField] private float dashCoolDown;
    private float nextAttackTime;
    private float nextDashTime;
    private bool subscribeFlag;
    
    public override void onEnter()
    {
        InputManager.Instance.onDashAction += SwitchToDashState;
        InputManager.Instance.onAttackAction += SwitchToAttackState;
        InvokeAnimationState();
        startTime = Time.time;
        RefreshDirections();
    }

    private void SwitchToAttackState(object sender, EventArgs e)
    {
        if (Time.time > nextAttackTime)
        {
            nextAttackTime = Time.time + attackRate;
            GetStateManager().SwitchState(GetStateManager().GetState(States.Attack.ToString()));
        }
    }

    private void SwitchToDashState(object sender, EventArgs e)
    {
        if (Time.time > nextDashTime)
        {
            nextDashTime = Time.time + dashCoolDown;
            GetStateManager().SwitchState(GetStateManager().GetState(States.Dash.ToString()));
        }
    }

    public override void onUpdate()
    {
        RefreshDirections();
        if (directions != Vector2.zero)
        {
            GetStateManager().SwitchState(GetStateManager().GetState(States.Move.ToString()));
        }
    }

    private void RefreshDirections()
    {
        if (InputManager.Instance != null)
        {
            directions = InputManager.Instance.GetInputDirections();
        }
    }
    
    public override void onExit()
    {
        startTime = 0;
        InputManager.Instance.onDashAction -= SwitchToDashState;
        InputManager.Instance.onAttackAction -= SwitchToAttackState;
        directions = Vector2.zero;
    }

    private IEnumerator waitUntilInput()
    {
        yield return new WaitUntil(() => { return InputManager.Instance != null; });
        InputManager.Instance.onDashAction += SwitchToDashState;
        InputManager.Instance.onAttackAction += SwitchToAttackState;
    }
    
    
}
