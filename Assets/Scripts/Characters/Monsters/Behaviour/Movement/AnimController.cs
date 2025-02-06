using UnityEngine;

public class AnimController : MonoBehaviour
{
    private Animator animator;
    public void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void EffectRunAnimation(int xMove)
    {
        animator.SetInteger("xVelocity", xMove);
    }
}
