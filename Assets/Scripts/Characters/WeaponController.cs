using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public float damage;

    public void Awake ()
    {
        gameObject.SetActive(false);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IDamagable target) && !other.CompareTag("Player"))
        {
            Debug.Log("Coliision with enemy registred");
            target.TakeDamage(damage);
        }
    }
}
