using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    [SerializeField] private CombatSo combatStats;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private int numberOfAttacks;
    private bool attacked;
    private bool animationInvoked;
    private float hitTimer;
    private int currentAttack;
    public event Action<int> attackAnimationEvent;
    
    public override void onEnter()
    {
        if (!CheckAttackArea())
        {
            GetStateManager().SwitchState(GetStateManager().GetState(States.Chase.ToString()));
        }
        else
        {
            startTime = Time.time;
        }
    }


    public override void onUpdate()
    {
        if (CheckAttackArea())
        {
            Freeze();
            
            if (!animationInvoked)
            { 
                animationInvoked = true;
                InvokeAttackStateAnimation(currentAttack);
                currentAttack = (currentAttack + 1) % numberOfAttacks;
            }
            hitTimer += Time.deltaTime;
            if (hitTimer > combatStats.attackAnimationHitTime && !attacked)
            { 
                attacked = true;
                Attack();
            }
            else if (hitTimer > combatStats.attackAnimationRemainingTime)
            {
                GetStateManager().SwitchState(GetStateManager().GetState(States.Idle.ToString()));
            }

        }
        else
        {
            UnFreeze();
            GetStateManager().SwitchState(GetStateManager().GetState(States.Chase.ToString()));
        }
        
    }


    public override void onExit()
    {
        startTime = 0;
        animationInvoked = false;
        attacked = false;
        hitTimer = 0;
        UnFreeze();
    }
    
    
    
    private bool CheckAttackArea()
    {
        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, combatStats.attack1Range, opponentMask);
        if (hit != null)
        {
            float playerDirection = hit.transform.position.x - transform.position.x;
            InvokeAnimationState(playerDirection);
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Attack()
    {
        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, combatStats.attack1Range, opponentMask);
        if (hit != null)
        {
            hit.GetComponent<BaseHealthScript>().TakeDamage(combatStats.damage);
        }
    }
    
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position,combatStats.attack1Range);
    }

    private void Freeze()
    {
        rd.constraints |= RigidbodyConstraints2D.FreezePosition;
    }

    private void UnFreeze()
    {
        rd.constraints &= ~RigidbodyConstraints2D.FreezePosition;
    }


    private void InvokeAttackStateAnimation(int order)
    {
        attackAnimationEvent?.Invoke(order);
    }
   
}
