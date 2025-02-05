using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject character;
    private float damage;
    private Collider2D characterCollider;
    private Character characterScript;

    public void Awake ()
    {
        characterScript = character.GetComponent<Character>();
        characterCollider = gameObject.GetComponent<Collider2D>();
        damage = characterScript.damage;

        gameObject.SetActive(false);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IDamagable target) && other != characterCollider)
        {
            Debug.Log("Detected: " + other.name);
            target.TakeDamage(damage);
        }
    }
}
