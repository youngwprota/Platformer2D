using UnityEngine;

public class Mashroom : Character
{

    public override void TakeDamage(float dagame)
    {
        if (IsDead)
            return;

        health -= damage;
        if (health <= 0)
            IsDead = true;

        Debug.Log($"Mashroom's health: {health}");
    }
}
