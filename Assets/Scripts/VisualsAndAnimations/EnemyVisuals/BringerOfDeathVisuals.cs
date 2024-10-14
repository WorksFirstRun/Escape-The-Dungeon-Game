using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringerOfDeathVisuals : MonoBehaviour
{
   private const string TAKEDAMAGE = "TakeDamage";
   private const string IS_MOVING = "isMoving";
   private const string ATTACK = "Attack";
   private const string DIE = "Die"; 
   private const string CAST_SPELL = "Cast";
    [SerializeField] private Animator animator;
    [SerializeField] private BaseHealthScript entityHealth;
    [SerializeField] private StateManager entityStateManager;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private AttackState attackState;
    
    private void Start()
    {
        entityHealth.onTakeDamageAnimation += DamageAnimationTriger;
        entityHealth.onDeathAnimation += EntityHealthOnonDeathAnimation;
        entityStateManager.GetState(States.Roam.ToString()).animationState += OnMoveAnimation;
        entityStateManager.GetState(States.Idle.ToString()).animationState += onIdleStopAll;
        entityStateManager.GetState(States.Chase.ToString()).animationState += OnMoveAnimation;
        entityStateManager.GetState(States.Roam.ToString()).animationStateWithOrder += FlipTheSprite;
        entityStateManager.GetState(States.Chase.ToString()).animationStateWithOrder += FlipTheSprite;
        entityStateManager.GetState(States.Attack.ToString()).animationStateWithOrder += FlipTheSprite;
        entityStateManager.GetState(States.Cast.ToString()).animationState += CastAnimation;
        attackState.attackAnimationEvent += AttackStateOnattackAnimationEvent;
    }

    private void AttackStateOnattackAnimationEvent(int obj)
    {
        StopAll();
        animator.SetTrigger(ATTACK);
    }

    private void CastAnimation(object sender, EventArgs e)
    {
        StopAll();
        animator.SetTrigger(CAST_SPELL);
    }

    private void EntityHealthOnonDeathAnimation(object sender, EventArgs e)
    {
        StopAll();
        animator.SetTrigger(DIE);
    }
    
    
    private void FlipTheSprite(float obj)
    {
        if (obj < 0)
        {
            sprite.flipX = false;
            if (attackPoint.localPosition.x > 0)
            {
                Vector3 attackPos = attackPoint.localPosition;
                attackPos.x *= -1;
                attackPoint.localPosition = attackPos;
            }
            
        }
        else if (obj > 0)
        {
            sprite.flipX = true;
            if (attackPoint.localPosition.x < 0)
            {
                Vector3 attackPos = attackPoint.localPosition;
                attackPos.x *= -1;
                attackPoint.localPosition = attackPos;
            }
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
        animator.SetTrigger(TAKEDAMAGE);
    }

    private void StopAll()
    {
        animator.SetBool(IS_MOVING,false);
    }
}
