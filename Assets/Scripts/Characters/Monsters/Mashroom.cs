using UnityEngine;

public class Mashroom : Character
{
    public override void TakeDamage(float dmg)
    {
        if (IsDead)
            return;

        health -= dmg;
        if (health <= 0)
            IsDead = true;

        Debug.Log($"Mashroom's health: {health}");
    }
}
