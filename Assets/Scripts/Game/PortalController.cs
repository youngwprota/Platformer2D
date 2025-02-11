using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalController : MonoBehaviour
{
    private Collider2D selfCollider;
    public void Awake()
    {
        selfCollider = GetComponent<Collider2D>();
        selfCollider.enabled = false;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            if (SceneManager.GetActiveScene().name == "Level1")
                SceneManager.LoadScene("Level2");
            if (SceneManager.GetActiveScene().name == "Level2")
                SceneManager.LoadScene("End");
        }
    }
}
