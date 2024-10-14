using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightBorneVisuals : MonoBehaviour
{
    private const string TAKEDAMAGE = "TakeDamage";
    private const string IS_MOVING = "isMoving";
    private const string IS_RUNNING = "isRunning";
    private const string ATTACK = "Attack";
    private const string DIE = "Die";
    [SerializeField] private Animator animator;
    [SerializeField] private BaseHealthScript entityHealth;
    [SerializeField] private StateManager entityStateManager;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private AttackState attackState;

    private void Start()
    {
        entityHealth.onTakeDamageAnimation += DamageAnimationTriger;
        entityHealth.onDeathAnimation += EntityHealthOnonDeathAnimation;
        entityStateManager.GetState(States.Roam.ToString()).animationState += OnMoveAnimation;
        entityStateManager.GetState(States.Idle.ToString()).animationState += onIdleStopAll;
        entityStateManager.GetState(States.Chase.ToString()).animationState += OnRunAnimation;
        entityStateManager.GetState(States.Roam.ToString()).animationStateWithOrder += FlipTheSprite;
        entityStateManager.GetState(States.Chase.ToString()).animationStateWithOrder += FlipTheSprite;
        entityStateManager.GetState(States.Attack.ToString()).animationStateWithOrder += FlipTheSprite;
        attackState.attackAnimationEvent += OnAttackAnimation;
    }

    private void EntityHealthOnonDeathAnimation(object sender, EventArgs e)
    {
        StopAll();
        animator.SetTrigger(DIE);
    }

    private void OnAttackAnimation(int obj)
    {
        StopAll();
        animator.SetTrigger(ATTACK);
    }

    private void OnRunAnimation(object sender, EventArgs e)
    {
        StopAll();
        animator.SetBool(IS_RUNNING, true);
    }

    private void FlipTheSprite(float obj)
    {
        if (obj < 0)
        {
            sprite.flipX = true;
        }
        else if (obj > 0)
        {
            sprite.flipX = false;
        }
    }

    private void OnMoveAnimation(object sender, EventArgs e)
    {
        StopAll();
        animator.SetBool(IS_MOVING,true);
    }


    private void onIdleStopAll(object sender, EventArgs e)
    {
        StopAll();
    }

    private void DamageAnimationTriger(object sender, EventArgs e)
    {
        StopAll();
        animator.SetTrigger(TAKEDAMAGE);
    }

    private void StopAll()
    {
        animator.SetBool(IS_MOVING,false);
        animator.SetBool(IS_RUNNING,false);
    }
}
