using UnityEngine;
using System.Collections;

public class EnemyRespawn : MonoBehaviour
{
    [Header("Som de morte")]
    public AudioClip somMorte;
    [Range(0f,1f)] public float volume = 1f;

    private Vector3 startPosition;
    private SpriteRenderer sprite;
    private Collider2D[] colliders;

    private void Start()
    {
        startPosition = transform.position;
        sprite = GetComponent<SpriteRenderer>();
        colliders = GetComponentsInChildren<Collider2D>(true);
    }

    public void KillEnemy(Collider2D player)
    {
        // --- ADIÇÃO DA LÓGICA DE STOMP (IMPEDIR MORTE LATERAL) ---
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();

        // Player não está descendo → não mata inimigo
        if (rb.linearVelocity.y >= 0)
            return;

        // Player não está acima da cabeça → não mata inimigo
        if (player.transform.position.y <= transform.position.y)
            return;
        // -----------------------------------------------------------

        // Player quica
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 8f);

        // Desativa gráficos e colisores
        foreach (var c in colliders)
            c.enabled = false;

        sprite.enabled = false;
        
        if (somMorte != null)
        {
            GameObject temp = new GameObject("SomDeMorteTemp");
            temp.transform.position = transform.position;

            AudioSource a = temp.AddComponent<AudioSource>();
            a.clip = somMorte;
            a.volume = volume;
            a.spatialBlend = 0f; // som 2D
            a.Play();

            Destroy(temp, somMorte.length + 0.1f);  
        }

        // Inicia o respawn
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(3f);

        transform.position = startPosition;

        // Reativa tudo
        sprite.enabled = true;
        foreach (var c in colliders)
            c.enabled = true;
    }
}
