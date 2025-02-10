using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBar : MonoBehaviour
{
    [SerializeField] private Character character;

    private float curHealth;
    private float kf;

    void Start()
    {
        transform.localScale = new Vector3(1, 1, 1);
        kf = 1 / character.health;
    }

    void Update()
    {
        curHealth = character.health;
        transform.localScale = new Vector3(kf * curHealth, 1, 1);
    }
}
