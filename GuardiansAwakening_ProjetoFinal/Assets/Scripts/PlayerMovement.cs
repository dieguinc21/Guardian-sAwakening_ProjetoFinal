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
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    [Header("Referências")]
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private bool isGrounded;

    // Posição inicial para respawn
    private Vector3 posInicial;

    // ------------------ Checkpoint ------------------
    private Vector3 checkpointPos;
    private bool checkpointAtivo = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        pulosRestantes = maxPulos;

        posInicial = transform.position;
        checkpointPos = posInicial;

        StartCoroutine(EfeitoPiscarInicio());
    }

    void Update()
    {
        // GROUND CHECK
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

    // ------------------ DETECÇÃO DE MORTE ------------------
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Morte"))
        {
            RespawnPlayer();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Morte"))
        {
            RespawnPlayer();
        }
    }

    // ------------------ CHECKPOINT FUNCTIONS ------------------
    public void SetCheckpoint(Vector3 pos, SpriteRenderer checkpointSprite = null)
    {
        checkpointPos = pos;
        checkpointAtivo = true;
        //Debug.Log("Checkpoint salvo: " + checkpointPos);

        // Se foi passado o sprite do checkpoint, faz a piscada
        if (checkpointSprite != null)
        {
            StartCoroutine(PiscarCheckpoint(checkpointSprite));
        }
    }

    private IEnumerator PiscarCheckpoint(SpriteRenderer checkpointSprite)
    {
        float duracao = 0.10f;
        int vezes = 3;
        Color originalColor = checkpointSprite.color;

        for (int i = 0; i < vezes; i++)
        {
            checkpointSprite.color = new Color(10f, 10f, 10f, 0f); // mais claro
            yield return new WaitForSeconds(duracao);
            checkpointSprite.color = originalColor;
            yield return new WaitForSeconds(duracao);
        }
    }

    public void RespawnPlayer()
    {
        Hearts hearts = GetComponent<Hearts>();
        if (hearts != null)
        {
            hearts.vida -= 1;
            if (hearts.vida < 0) hearts.vida = 0;

            // Se vida zerou → GameOver
            if (hearts.vida == 0)
            {
                checkpointPos = posInicial;
                checkpointAtivo = false;
                //Debug.Log("Game Over! Player sem vidas.");
                SceneManager.LoadScene("GameOver");
                return;
            }
        }

        // Respawn normal no último checkpoint ou posição inicial
        rb.linearVelocity = Vector2.zero;

        Vector3 spawnPos = checkpointAtivo ? checkpointPos : posInicial;

        Collider2D playerCollider = GetComponent<Collider2D>();
        float alturaPlayer = playerCollider != null ? playerCollider.bounds.size.y : 1f;

        spawnPos.y += alturaPlayer / 2f + 0.1f;
        transform.position = spawnPos;

        pulosRestantes = maxPulos;

        StartCoroutine(EfeitoPiscarInicio());
    }

    // ------------------ EFEITO VISUAL AO MORRER / SPAWN ------------------
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
