using UnityEngine;

public class PlayerAT : MonoBehaviour
{
    [Header("Movimento")]
    public float speed = 5f;
    public float jumpForce = 7f;
    private Rigidbody2D rb;
    private bool isGrounded;

    [Header("Tiro")]
    public GameObject playerBulletPrefab;
    public Transform firePoint;

    [Header("Vida")]
    public int vida = 3;
    public Hearts heartsScript; // caso use outro sistema de vida, me avise

    [Header("Componentes")]
    private Animator anim;
    private SpriteRenderer sr;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        if (heartsScript != null)
            heartsScript.vida = vida;
    }

    void Update()
    {
        Movimentar();
        Pular();
        Atirar();
    }

    // ===============================================================
    // MOVIMENTO
    // ===============================================================
    void Movimentar()
    {
        float movimento = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(movimento * speed, rb.linearVelocity.y);

        // animação
        anim.SetFloat("speed", Mathf.Abs(movimento));

        // flip
        if (movimento > 0) sr.flipX = false;
        if (movimento < 0) sr.flipX = true;
    }

    // ===============================================================
    // PULO
    // ===============================================================
    void Pular()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            anim.SetTrigger("pular");
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Chao"))
            isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.collider.CompareTag("Chao"))
            isGrounded = false;
    }

    // ===============================================================
    // TIRO — TECLA T
    // ===============================================================
    void Atirar()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            anim.SetTrigger("atirar");

            GameObject b = Instantiate(playerBulletPrefab, firePoint.position, firePoint.rotation);

            // se o sprite estiver virado, o tiro vira também
            if (sr.flipX)
            {
                Vector3 scale = b.transform.localScale;
                scale.x *= -1;
                b.transform.localScale = scale;
            }
        }
    }

    // ===============================================================
    // DANO NO PLAYER
    // ===============================================================
    public void TomarDano(int dano)
    {
        vida -= dano;

        if (heartsScript != null)
            heartsScript.vida = vida;

        if (vida <= 0)
            Morrer();
    }

    void Morrer()
    {
        anim.SetTrigger("morrer");
        rb.linearVelocity = Vector2.zero;
        this.enabled = false;

        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
    }

    // ===============================================================
    // DANO POR PROJETIL DO BOSS
    // ===============================================================
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("InimigoTiro"))
        {
            TomarDano(1);
            Destroy(col.gameObject);
        }
    }
}
