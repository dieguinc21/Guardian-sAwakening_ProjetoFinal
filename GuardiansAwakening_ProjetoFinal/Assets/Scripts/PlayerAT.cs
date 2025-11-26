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


    void Movimentar()
    {
        float movimento = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(movimento * speed, rb.linearVelocity.y);

    
        anim.SetFloat("speed", Mathf.Abs(movimento));

        
        if (movimento > 0) sr.flipX = false;
        if (movimento < 0) sr.flipX = true;
    }


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
        if (col.collider.CompareTag("Ground"))
            isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.collider.CompareTag("Ground"))
            isGrounded = false;
    }

    void Atirar()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            anim.SetTrigger("atirar");

            GameObject b = Instantiate(playerBulletPrefab, firePoint.position, firePoint.rotation);

            if (sr.flipX)
            {
                Vector3 scale = b.transform.localScale;
                scale.x *= -1;
                b.transform.localScale = scale;
            }
        }
    }

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

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("InimigoTiro"))
        {
            TomarDano(1);
            Destroy(col.gameObject);
        }
    }
}
