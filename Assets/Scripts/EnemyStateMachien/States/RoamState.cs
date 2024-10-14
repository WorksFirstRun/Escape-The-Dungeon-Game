using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoamState : BaseState
{
    [SerializeField] private MovementSO movementStats;
    [SerializeField] private TrigerAreaSO areas;
    [SerializeField] private Transform chasePoint;
    [SerializeField] private float pointDistanceThreshHold;
    [SerializeField] private Transform castSpellPoint;
    [SerializeField] private float castSpellCoolDown;
    private float spellCoolDown;
    
    private Vector2 direction;
    private Vector2 randomPoint;
    private bool isReached;
    private bool animationsStart;
    private float pointDistance;
    
    
    public override void onEnter()
    {
        PickRandomPoint();
        startTime = Time.time;
    }


    public override void onUpdate()
    {
        
        
        if (areas.CheckArea(chasePoint.position,areas.chaseArea,opponentMask))
        {
           GetStateManager().SwitchState(GetStateManager().GetState(States.Chase.ToString()));
        }

        else if (castSpellPoint is not null && areas.CheckArea(castSpellPoint.position, areas.castSpellArea, opponentMask) && Time.time > spellCoolDown)
        {
            spellCoolDown = Time.time + castSpellCoolDown;
            GetStateManager().SwitchState(GetStateManager().GetState(States.Cast.ToString()));
        }
        
        else
        {
            if (!animationsStart)
            {
                InvokeAnimationState();
                animationsStart = true;
            }
                    
            Move();
            CheckIfReached();
                    
            if (isReached)
            {
                GetStateManager().SwitchState(GetStateManager().GetState(States.Idle.ToString()));
            }
        }
        
    }


    public override void onExit()
    {
        isReached = false;
        startTime = 0;
        animationsStart = false;
        rd.velocity = Vector2.zero;
    }

    private void PickRandomPoint()
    {
        float randomAngel = Random.Range(0, Mathf.PI * 2);
        float distance = 5f;
        randomPoint = new Vector2(Mathf.Cos(randomAngel) * distance + transform.position.x, Mathf.Sin(randomAngel) * distance + transform.position.y);
        direction = new Vector2(randomPoint.x - transform.position.x,randomPoint.y - transform.position.y).normalized;
        InvokeAnimationState(direction.x);
    }

    private void Move()
    {
        rd.velocity = direction * movementStats.speed;
    }

    private void CheckIfReached()
    { 
        pointDistance = Mathf.Sqrt(Mathf.Pow(randomPoint.x - transform.position.x,2) + Mathf.Pow(randomPoint.y - transform.position.y,2));
        if (pointDistance < pointDistanceThreshHold)
        {
            isReached = true;
        }
        else if (time > 3)
        {
            GetStateManager().SwitchState(GetStateManager().GetState(States.Idle.ToString()));
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, randomPoint);
        Gizmos.DrawWireSphere(chasePoint.position,areas.chaseArea);
        Gizmos.DrawWireSphere(castSpellPoint.position,areas.castSpellArea);
    }

   
}
