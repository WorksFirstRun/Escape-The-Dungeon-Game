using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBackState : BaseState
{
    [SerializeField] private float strength, delay;
    private Transform playerPosition;
    private Vector2 direction;
    private bool knocked;
    
    public override void onEnter()
    {
        if (PlayerStateManager.Instance != null)
        {
            playerPosition = PlayerStateManager.Instance.transform;
        }

        startTime = Time.time;
        direction = (transform.position - playerPosition.position).normalized;
    }

    public override void onUpdate()
    {
        if (!knocked)
        {
            rd.AddForce(direction * strength, ForceMode2D.Impulse);
            knocked = true;
        }
        
        if (time >= delay)
        {
            GetStateManager().SwitchState(GetStateManager().GetState(States.Idle.ToString()));
        }
    }

    public override void onExit()
    {
        playerPosition = null;
        knocked = false;
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
