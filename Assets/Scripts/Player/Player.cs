using UnityEngine;

public class Player : Character
{

    [Header("Player Stats")]
    public float jumpHeight;
    public float dashCooldown;
    public float dashValue;

    public override void TakeDamage(float damage)
    {
        if (health <= 0)
            return;
        health -= damage;
    }
    

}
