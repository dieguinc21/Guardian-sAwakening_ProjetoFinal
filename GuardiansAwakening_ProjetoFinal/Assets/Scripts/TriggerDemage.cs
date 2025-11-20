using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerDemage : MonoBehaviour
{
    public Hearts heart;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            heart.vida--;

            if (heart.vida <= 0)
            {
                SceneManager.LoadScene("GameOver");
            }
        }
    }
}
