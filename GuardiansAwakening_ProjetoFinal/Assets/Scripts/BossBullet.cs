using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float speed = 7f;

    void Start()
    {
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        // Movimento já é dado pelo Rigidbody (BossController)
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            Hearts h = collision.GetComponent<Hearts>();

            if (h != null)
            {
                h.vida -= 1;
                if (h.vida <= 0)
                    UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
            }
        }

        if (!collision.CompareTag("Boss"))
            Destroy(gameObject);
    }
}
