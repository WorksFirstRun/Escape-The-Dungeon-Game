using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisuals : MonoBehaviour
{
    private const string IS_WALKING = "isWalking";
    private const string IS_JUMPING = "isJumping";
    private const string IS_FALLING = "isFalling";
    private const string IS_DASHING = "isDashing";
    private const string ATTACK1 = "Attack1";
    private const string ATTACK2 = "Attack2";
    private const string ATTACK3 = "Attack3";
    private const string TAKEDAMAGE = "TakeDamage";
    [SerializeField] private SpriteRenderer playerSprite;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private PlayerStateManager entityState;
    [SerializeField] private Transform attackPoint;
    
    private void Start()
    {
        playerHealth.onTakeDamageAnimation += TakeDamageAnimation;
        entityState.GetState(States.Idle.ToString()).animationState += IdleStateAnimation;
        entityState.GetState(States.Move.ToString()).animationState += MoveStateAnimation;
        entityState.GetState(States.Dash.ToString()).animationState += DashStateAnimation;
        entityState.GetState(States.Attack.ToString()).animationStateWithOrder += AttackAnimation;
        entityState.GetState(States.Move.ToString()).animationStateWithOrder += OnAnimationStateWithOrder;
        
    }

    private void AttackAnimation(float obj)
    {
        if (obj == 0)
        {
            playerAnimator.SetTrigger(ATTACK1);
        }
        else if (obj == 1)
        {
            playerAnimator.SetTrigger(ATTACK2);
        }
        else if (obj == 2)
        {
            playerAnimator.SetTrigger(ATTACK3);
        }
    }


    private void DashStateAnimation(object sender, EventArgs e)
    {
        StopAll();
        playerAnimator.SetBool(IS_DASHING,true);
    }

    private void OnAnimationStateWithOrder(float obj)
    {
        if (obj < 0)
        {
            playerSprite.flipX = true;
            if (attackPoint.localPosition.x > 0)
            {
                Vector3 attackPos = attackPoint.localPosition;
                attackPos.x *= -1;
                attackPoint.localPosition = attackPos;
            }
        }
        else if (obj > 0)
        {
            playerSprite.flipX = false;
            if (attackPoint.localPosition.x < 0)
            {
                Vector3 attackPos = attackPoint.localPosition;
                attackPos.x *= -1;
                attackPoint.localPosition = attackPos;
            }
        }
    }

    private void MoveStateAnimation(object sender, EventArgs e)
    {
        StopAll();
        playerAnimator.SetBool(IS_WALKING, true);
    }

    private void IdleStateAnimation(object sender, EventArgs e)
    {
        StopAll();
    }

    private void TakeDamageAnimation(object sender, EventArgs e)
    {
        playerAnimator.SetTrigger(TAKEDAMAGE);
    }
    
    private void StopAll()
    {
        playerAnimator.SetBool(IS_WALKING, false);
        playerAnimator.SetBool(IS_FALLING, false);
        playerAnimator.SetBool(IS_JUMPING, false);
        playerAnimator.SetBool(IS_DASHING, false);
    }
}
