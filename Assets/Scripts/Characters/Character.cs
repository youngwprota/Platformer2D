using UnityEngine;

public abstract class Character : MonoBehaviour, IDamagable
{   
    [Header("Character Stats")]
    public float health;
    public float damage;
    public float speed;

    public int countKills = 0;

    public bool IsDead {get ; protected set;} = false;

    public abstract void TakeDamage(float damage);
}
