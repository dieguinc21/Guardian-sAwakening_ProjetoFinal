using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movimento")]
    public float speed = 5f;
    public float jumpForce = 7f;

    [Header("Pulo Duplo")]
    public int maxPulos = 2;
    private int pulosRestantes;

    [Header("Referências")]
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        pulosRestantes = maxPulos;

        // Faz o player piscar ao iniciar a cena (3 piscadas rápidas)
        StartCoroutine(EfeitoPiscarInicio());
    }

    void Update()
    {
        float move = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(move * speed, rb.linearVelocity.y);

        // Pulo duplo
        if (Input.GetButtonDown("Jump") && pulosRestantes > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            pulosRestantes--;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Detecta o chão
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            pulosRestantes = maxPulos;
        }

        // Detecta o tilemap de morte
        if (collision.gameObject.CompareTag("Morte"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private IEnumerator EfeitoPiscarInicio()
    {
        float duracao = 0.10f; // duração de cada piscada (rápido)
        int quantidade = 3;    // número de piscadas

        for (int i = 0; i < quantidade; i++)
        {
            spriteRenderer.color = new Color(1f, 1f, 1f, 0f); // semi-transparente
            yield return new WaitForSeconds(duracao);
            spriteRenderer.color = new Color(1f, 1f, 1f, 1f);   // normal
            yield return new WaitForSeconds(duracao);
        }
    }
}