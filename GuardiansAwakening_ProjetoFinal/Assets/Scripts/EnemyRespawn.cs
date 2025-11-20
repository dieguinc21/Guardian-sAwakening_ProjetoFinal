using UnityEngine;
using System.Collections;

public class EnemyRespawn : MonoBehaviour
{
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
        // Player quica
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 8f);

        // Desativa gráficos e colisores, MAS mantém o GameObject ativo
        foreach (var c in colliders)
            c.enabled = false;

        sprite.enabled = false;

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
