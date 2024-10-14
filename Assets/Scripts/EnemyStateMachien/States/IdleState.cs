using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState
{
    [SerializeField] private TrigerAreaSO areas;
    [SerializeField] private Transform chasePoint;
    [SerializeField] private Transform castSpellPoint;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private CombatSo combatStats;
    private float spellCoolDown;
    private float attackCoolDown;
    public override void onEnter()
    {
        startTime = Time.time;
        InvokeAnimationState();
    }


    public override void onUpdate()
    {
        if (time >= 3)
        {
            GetStateManager().SwitchState(GetStateManager().GetState(States.Roam.ToString()));
        }
        else if (areas.CheckArea(attackPoint.position, areas.attackArea, opponentMask) && Time.time > attackCoolDown)
        {
            attackCoolDown = Time.time + combatStats.attackRate;
            GetStateManager().SwitchState(GetStateManager().GetState(States.Attack.ToString()));
        }
        else if (areas.CheckArea(chasePoint.position,areas.chaseArea,opponentMask) && !areas.CheckArea(attackPoint.position, areas.attackArea, opponentMask))
        {
            GetStateManager().SwitchState(GetStateManager().GetState(States.Chase.ToString()));
        }
        else if (areas.CheckArea(castSpellPoint.position, areas.castSpellArea, opponentMask) && Time.time > spellCoolDown
                 && !areas.CheckArea(attackPoint.position, areas.attackArea, opponentMask))
        {
            spellCoolDown = Time.time + combatStats.castCoolDown;
            GetStateManager().SwitchState(GetStateManager().GetState(States.Cast.ToString()));
        }
    }

    public override void onExit()
    {
        startTime = 0;
    }
    
   
    
}
