using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowFireVisuals : MonoBehaviour
{
    private const string THROW = "Throw";
    [SerializeField] private FlameThrower flameThrower;
    [SerializeField] private Animator animator;
    private void Start()
    {
        flameThrower.throwFlameAnimationEvent += ThrowAnimation;
    }

    private void ThrowAnimation()
    {
        animator.SetTrigger(THROW);
    }
}
