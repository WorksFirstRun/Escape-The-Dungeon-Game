using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : BaseState
{
    private Vector2 directions;
    [SerializeField] private MovementSO movmentStats;
    [SerializeField] private CapsuleCollider2D playerCollisionBox;
    [SerializeField] private CapsuleCollider2D playerWallCollisionBox;
    
    public override void onEnter()
    {
        startTime = Time.time;
        directions = InputManager.Instance.GetInputDirections();
        InvokeAnimationState();
        Dash();
    }

    public override void onUpdate()
    {
        if (!CanDash())
        {
            GetStateManager().SwitchState(GetStateManager().GetState(States.Idle.ToString()));
        }
    }

    public override void onExit()
    {
        rd.velocity = directions = Vector2.zero;
        ResetCollisionBox();
        startTime = 0;
    }

    private bool CanDash()
    {
        return time <= movmentStats.dashingDuration;
    }

    private void Dash()
    {
        playerCollisionBox.size = new Vector2(playerCollisionBox.size.x, 1.08f);
        playerWallCollisionBox.size = new Vector2(playerWallCollisionBox.size.x, 1.08f);
        playerCollisionBox.offset = new Vector2(playerCollisionBox.offset.x, -0.59f);
        playerWallCollisionBox.offset = new Vector2(playerWallCollisionBox.offset.x, -0.59f);
            
        rd.velocity = directions * movmentStats.dashingSpeed;
    }

    private void ResetCollisionBox()
    {
        playerCollisionBox.size = new Vector2(playerCollisionBox.size.x, 1.73f);
        playerWallCollisionBox.size = new Vector2(playerWallCollisionBox.size.x, 1.73f);
        playerCollisionBox.offset = new Vector2(playerCollisionBox.offset.x, 0.04f);
        playerWallCollisionBox.offset = new Vector2(playerWallCollisionBox.offset.x, 0.04f);
    }
}
