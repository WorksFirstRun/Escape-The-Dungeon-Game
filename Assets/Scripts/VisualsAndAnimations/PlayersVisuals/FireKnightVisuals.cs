using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireKnightVisuals : MonoBehaviour
{
    private const string IS_MOVING = "isMoving";
    private const string ROLL = "Roll";
    private const string ATTACK1 = "Attack1";
    private const string ATTACK2 = "Attack2";
    private const string ATTACK3 = "Attack3";
    private const string TAKEDAMAGE = "TakeDamage";
    private const string SPECIAL_ATTACK = "SpecialAttack";
    private const string DIE = "Die";
    private const string IS_DEFENDING = "isDefending";
    [SerializeField] private SpriteRenderer playerSprite;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private PlayerStateManager entityState;
    [SerializeField] private Transform[] attackPoints;
    
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
            StopAll();
            playerAnimator.SetTrigger(ATTACK1);
        }
        else if (obj == 1)
        {
            StopAll();
            playerAnimator.SetTrigger(ATTACK2);
        }
        else if (obj == 2)
        {
            StopAll();
            playerAnimator.SetTrigger(ATTACK3);
        }
    }


    private void DashStateAnimation(object sender, EventArgs e)
    {
        StopAll();
        playerAnimator.SetTrigger(ROLL);
    }

    private void OnAnimationStateWithOrder(float obj)
    {
        if (obj < 0)
        {
            playerSprite.flipX = true;
            foreach (Transform point in attackPoints)
            {
                if (point.localPosition.x > 0)
                {
                    Vector3 attackPos = point.localPosition;
                    attackPos.x *= -1;
                    point.localPosition = attackPos;
                }
            }
            
        }
        else if (obj > 0)
        {
            playerSprite.flipX = false;
            foreach (Transform point in attackPoints)
            {
                if (point.localPosition.x < 0)
                {
                    Vector3 attackPos = point.localPosition;
                    attackPos.x *= -1;
                    point.localPosition = attackPos;
                }
            }
        }
    }

    private void MoveStateAnimation(object sender, EventArgs e)
    {
        StopAll();
        playerAnimator.SetBool(IS_MOVING, true);
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
        playerAnimator.SetBool(IS_MOVING, false);
        playerAnimator.SetBool(IS_DEFENDING, false);
    }
}
