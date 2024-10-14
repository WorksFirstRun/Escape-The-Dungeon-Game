using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : BaseState
{
    [Serializable]
    private struct AttackInformation
    {
        public Transform attackPoint;
        public float backStateTimers;
        public float hitTime;
    }

    [SerializeField] private AttackInformation[] attacksList;
    [SerializeField] private CombatSo combatStats;
    private float attackOrder;
    private float attackOrderResetDuration = 2f;
    private Coroutine attackOrderResetTimer;
    private float backTimer;
    private bool attacked;
    public override void onEnter()
    {
        Freeze();
        InvokeAnimationState(attackOrder);
        if (attackOrderResetTimer != null)
        {
            StopCoroutine(attackOrderResetTimer);
        }
        attackOrderResetTimer = StartCoroutine(ResetAttackOrder());
        
    }

    public override void onUpdate()
    {
        backTimer += Time.deltaTime;
        if (backTimer > attacksList[(int) attackOrder].hitTime && !attacked)
        {
            Attack(attackOrder);
            attacked = true;
        }
        else if (backTimer > attacksList[(int) attackOrder].backStateTimers)
        {
            attackOrder = (attackOrder + 1) % attacksList.Length;
            GetStateManager().SwitchState(GetStateManager().GetState(States.Idle.ToString()));
        }
    }

    public override void onExit()
    {
        attacked = false;
        backTimer = 0;
        UnFreeze();
    }

    private void Attack(float attackNumber)
    {
        if (attackNumber == 0)
        {
            Attack1();
        }
        else if (attackNumber == 1)
        {
            Attack2();
        }
        else if (attackNumber == 2)
        {
            Attack3();
        }
    }
    
    private void Attack1()
    {
        Collider2D[] attackedObjects = Physics2D.OverlapCircleAll(attacksList[0].attackPoint.position, combatStats.attack1Range, opponentMask);
        StartCoroutine(DamageEnemies(attackedObjects));
    }
    
    private void Attack2()
    {
        Vector2 size = new Vector2(combatStats.attack2Range, 0.865f);
        CapsuleDirection2D direction = CapsuleDirection2D.Horizontal;
        Collider2D[] attackedObjects =
            Physics2D.OverlapCapsuleAll(attacksList[1].attackPoint.position, size, direction, 0f, opponentMask);
        StartCoroutine(DamageEnemies(attackedObjects));
    }
    
    private void Attack3()
    {
        Collider2D[] attackedObjects = Physics2D.OverlapCircleAll(attacksList[2].attackPoint.position, combatStats.attack3Range, opponentMask);
        StartCoroutine(DamageEnemies(attackedObjects));
    }
    

    private IEnumerator DamageEnemies(Collider2D[] AttackedObjects)
    {
        foreach (Collider2D h in AttackedObjects)
        {
            if (h.TryGetComponent(out BaseHealthScript Enemy))
            {
                Enemy.TakeDamage(combatStats.damage);
            }
        }

        yield return null;
    }

    private IEnumerator ResetAttackOrder()
    {
        yield return new WaitForSeconds(attackOrderResetDuration);
        attackOrder = 0;
    }

    
    
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attacksList[0].attackPoint.position,combatStats.attack1Range);
        Gizmos.DrawWireSphere(attacksList[2].attackPoint.position,combatStats.attack3Range);
        Vector2 size = new Vector2(combatStats.attack2Range, 0.865f);
        CapsuleDirection2D direction = CapsuleDirection2D.Horizontal;
        Vector3 center = attacksList[1].attackPoint.position;

        // Set the color for the gizmo
        Gizmos.color = Color.red;

        // Draw the capsule
        if (direction == CapsuleDirection2D.Horizontal)
        {
            // Draw two circles for the capsule ends
            Gizmos.DrawWireSphere(center - Vector3.right * (size.x / 2 - size.y / 2), size.y / 2);
            Gizmos.DrawWireSphere(center + Vector3.right * (size.x / 2 - size.y / 2), size.y / 2);

            // Draw the rectangle connecting the circles
            Gizmos.DrawLine(center - Vector3.right * (size.x / 2 - size.y / 2) + Vector3.up * (size.y / 2), 
                center + Vector3.right * (size.x / 2 - size.y / 2) + Vector3.up * (size.y / 2));
            Gizmos.DrawLine(center - Vector3.right * (size.x / 2 - size.y / 2) - Vector3.up * (size.y / 2), 
                center + Vector3.right * (size.x / 2 - size.y / 2) - Vector3.up * (size.y / 2));
        }
        else
        {
            // Draw two circles for the capsule ends in vertical direction
            Gizmos.DrawWireSphere(center - Vector3.up * (size.y / 2 - size.x / 2), size.x / 2);
            Gizmos.DrawWireSphere(center + Vector3.up * (size.y / 2 - size.x / 2), size.x / 2);

            // Draw the rectangle connecting the circles
            Gizmos.DrawLine(center - Vector3.up * (size.y / 2 - size.x / 2) + Vector3.right * (size.x / 2), 
                center + Vector3.up * (size.y / 2 - size.x / 2) + Vector3.right * (size.x / 2));
            Gizmos.DrawLine(center - Vector3.up * (size.y / 2 - size.x / 2) - Vector3.right * (size.x / 2), 
                center + Vector3.up * (size.y / 2 - size.x / 2) - Vector3.right * (size.x / 2));
        }
    }
    
    
    private void Freeze()
    {
        rd.constraints |= RigidbodyConstraints2D.FreezePosition;
    }

    private void UnFreeze()
    {
        rd.constraints &= ~RigidbodyConstraints2D.FreezePosition;
    }
}
