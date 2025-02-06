using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAnimationsController : MonoBehaviour
{
    private Animator animator;
    private int lastAttackIndex = 0;
    public void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void EffectRunAnimation(bool xMove)
    {
        animator.SetBool("xMove", xMove);
    }

    public void EffectJumpAnimation(bool onGround, float yVelocity)
    {
        animator.SetBool("onGround", onGround);
        animator.SetFloat("yVelocity", yVelocity);
    }

    public void EffectAttackAnimation()
    {
        int attackIndex  = (lastAttackIndex == 1) ? 2 : 1;
        animator.SetInteger("AttackIndex", attackIndex);
        animator.SetTrigger("AttackButtonDown");
        lastAttackIndex = attackIndex;
    }
    public void EffectDashAnimation()
    {
        animator.SetTrigger("Dash");
    }

    public void EffectDeathAnimation()
    {
        animator.SetTrigger("isDead");
    }
    public void StopAllAnimations()
    {
        GetComponent<Animator>().enabled = false; 
    }

}
