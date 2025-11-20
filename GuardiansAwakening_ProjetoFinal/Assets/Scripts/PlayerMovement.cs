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

    private Animator animator;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();

        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        pulosRestantes = maxPulos;

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

        // Animação de movimento
        animator.SetBool("movendo", move != 0);

        if (move > 0)
        {
            spriteRenderer.flipX = false;
        }
        if (move < 0)
        {
            spriteRenderer.flipX = true;
        }
        
        animator.SetBool("saltando", !isGrounded);    
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            pulosRestantes = maxPulos;
        }

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
        float duracao = 0.10f;
        int quantidade = 3;

        for (int i = 0; i < quantidade; i++)
        {
            spriteRenderer.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(duracao);
            spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(duracao);
        }
    }
}
