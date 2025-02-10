using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{

    public int needKills;

    [SerializeField] private Player player;
    [SerializeField] private Collider2D portal;



    void Update()
    {
        if (player.countKills == needKills)
        {
            portal.enabled = true;
        }
    }
}
