using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        if (sr == null)
        {
            //Debug.LogWarning("Checkpoint precisa de SpriteRenderer para piscar.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            PlayerMovement player = collision.GetComponent<PlayerMovement>();
            if (player != null)
            {
                // Passa o sprite para piscar
                player.SetCheckpoint(transform.position, sr);
            }
        }
    }
}
