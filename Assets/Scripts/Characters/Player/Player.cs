using UnityEngine;

public class Player : Character
{

    [Header("Player Stats")]
    public float jumpHeight;
    public float dashCooldown;
    public float dashValue;
    public override void TakeDamage(float dmg)
    {
        if (IsDead)
            return;

        health -= dmg;
        if (health <= 0)
            IsDead = true;

        Debug.Log($"Player's health: {health}");
    }
}
