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

    [Header("Ground Check")]
    public Transform groundCheck;            // Posição do sensor
    public float groundCheckRadius = 0.2f;   // Raio da detecção
    public LayerMask groundLayer;            // Camada do chão

    [Header("Referências")]
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private bool isGrounded;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        pulosRestantes = maxPulos;

        StartCoroutine(EfeitoPiscarInicio());
    }

    void Update()
    {
        
        // GROUND CHECK PRECISO(game obgect Vasio para detectar o chão)
        
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Reseta os pulos ao tocar no chão
        if (isGrounded)
            pulosRestantes = maxPulos;

        
        // MOVIMENTO
        
        float move = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(move * speed, rb.linearVelocity.y);

        // PULO / PULO DUPLO
 
        if (Input.GetButtonDown("Jump") && pulosRestantes > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            pulosRestantes--;
        }

        
        // ANIMAÇÕES
       
        animator.SetBool("movendo", move != 0);
        animator.SetBool("saltando", !isGrounded);

        
        // FLIP
        
        if (move > 0) spriteRenderer.flipX = false;
        if (move < 0) spriteRenderer.flipX = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Morte"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    // EFEITO VISUAL AO INICIAR
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
    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
