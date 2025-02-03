using UnityEngine;

public class Goblin : Character
{
    public override void TakeDamage(float dagame)
    {
        if (IsDead)
            return;

        health -= damage;
        if (health <= 0)
            IsDead = true;

        Debug.Log($"Goblin's health: {health}");
    }
}
