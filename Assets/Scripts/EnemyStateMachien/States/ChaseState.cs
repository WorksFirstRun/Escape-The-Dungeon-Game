using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : BaseState
{
    [SerializeField] private MovementSO movementStats;
    [SerializeField] private TrigerAreaSO areas;
    [SerializeField] private Transform chasePoint;
    [SerializeField] private Transform attackPoint;
    private Transform playerPosition;
    private Vector2 direction;
    private bool animationStart;
    
    public override void onEnter()
    {
        if (PlayerStateManager.Instance != null)
        {
            playerPosition = PlayerStateManager.Instance.transform;
        }
    }

    public override void onUpdate()
    {
        if (!animationStart)
        {
            InvokeAnimationState();
            animationStart = true;
        }
        if (areas.CheckArea(chasePoint.position,areas.chaseArea,opponentMask))
        {
            if (areas.CheckArea(attackPoint.position,areas.attackArea,opponentMask))
            {
                GetStateManager().SwitchState(GetStateManager().GetState(States.Idle.ToString()));
            }
            else
            {
                Move();
            }
        }
        else
        {
            GetStateManager().SwitchState(GetStateManager().GetState(States.Roam.ToString()));
        }
    }


    public override void onExit()
    {
        rd.velocity = direction = Vector2.zero;
        animationStart = false;
        playerPosition = null;
    }
    
   
    

    private void Move()
    {
        direction = new Vector2(playerPosition.position.x - transform.position.x,playerPosition.position.y - transform.position.y).normalized;
        InvokeAnimationState(direction.x);
        rd.velocity = direction * (movementStats.speed);
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(chasePoint.position,areas.chaseArea);
    }
}
