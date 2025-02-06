using UnityEngine;

public class Goblin : Character
{
    public override void TakeDamage(float dmg)
    {
        if (IsDead)
            return;

        health -= dmg;
        if (health <= 0)
            IsDead = true;

        Debug.Log($"Goblin's health: {health}");
    }
}
