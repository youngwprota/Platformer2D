using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public int needKills;

    [SerializeField] private Player player;
    [SerializeField] private Collider2D portal;

    void Update()
    {
        if (player.IsDead)
        {
            StartCoroutine(RestartLevel());
        }

        if (player.countKills == needKills)
        {
            portal.enabled = true;
        }
    }

    public IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(3f); 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }
}