using UnityEngine;

public abstract class Character : MonoBehaviour, IDamagable
{   
    [Header("Character Stats")]
    public float health;
    public float damage;
    public float speed;

    public abstract void TakeDamage(float damage);
}
