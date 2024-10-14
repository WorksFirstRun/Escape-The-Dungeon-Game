using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : BaseState
{
    private Vector2 directions;
    [SerializeField] private MovementSO movementStats;
    [SerializeField] private float attackRate; // should be the attackTime of the attack + the cooldown
    [SerializeField] private float dashCoolDown;
    private float nextAttackTime;
    private float nextDashTime;
    public override void onEnter()
    {
        InputManager.Instance.onDashAction += SwitchToDashState;
        InputManager.Instance.onAttackAction += SwitchToAttackState;
        startTime = Time.time;
        InvokeAnimationState();
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
            Move();
        }
        else
        {
            GetStateManager().SwitchState(GetStateManager().GetState(States.Idle.ToString()));
        }
    }

    public override void onExit()
    {
        InputManager.Instance.onDashAction -= SwitchToDashState;
        InputManager.Instance.onAttackAction -= SwitchToAttackState;
        startTime = 0;
        directions = Vector2.zero;
        rd.velocity = Vector2.zero;
    }
    
    private void RefreshDirections()
    {
        directions = InputManager.Instance.GetInputDirections();
    }

    private void Move()
    {
        InvokeAnimationState(directions.x);
        rd.velocity = directions * movementStats.speed;
    }
}
