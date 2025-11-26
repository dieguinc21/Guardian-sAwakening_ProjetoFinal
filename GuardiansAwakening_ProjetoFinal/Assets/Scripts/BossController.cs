using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossController : MonoBehaviour
{
    [Header("player")]
    public Transform player;
    public float detectionRange = 6f;

    [Header("Ataque")]
    public GameObject bossBulletPrefab;
    public Transform firePoint;
    public float fireRate = 1.2f;
    private float fireTimer = 0f;

    [Header("Animator")]
    private Animator animator;

    [Header("Vida do Boss")]
    public int vida = 6;
    public Image[] vidaHUD;

    [Header("Movimento")]
    public BossPatrolRaycast patrol;

    private bool playerDetectado = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        DetectarPlayer();

        if (playerDetectado)
            Atacar();
        else
            Patrulhar();
    }

    void DetectarPlayer()
    {
        float distancia = Vector2.Distance(transform.position, player.position);

        playerDetectado = distancia <= detectionRange;
    }

    void Atacar()
    {
        patrol.enabled = false;
        animator.SetBool("atacando", true);

        fireTimer += Time.deltaTime;

        if (fireTimer >= fireRate)
        {
            fireTimer = 0f;
            Atirar();
        }
    }

    void Patrulhar()
    {
        patrol.enabled = true;
        animator.SetBool("atacando", false);
    }

    void Atirar()
    {
        GameObject b = Instantiate(bossBulletPrefab, firePoint.position, Quaternion.identity);

        Rigidbody2D rb = b.GetComponent<Rigidbody2D>();

        Vector2 dir = (player.position - transform.position).normalized;
        rb.linearVelocity = dir * 6f;
    }

    public void TomarDano(int dano)
    {
        vida -= dano;
        if (vida < 0) vida = 0;

        AtualizarHUD();

        if (vida == 0)
        {
            StartCoroutine(Morrer());
        }
    }

    void AtualizarHUD()
    {
        for (int i = 0; i < vidaHUD.Length; i++)
        {
            vidaHUD[i].enabled = i < vida;
        }
    }

    private System.Collections.IEnumerator Morrer()
    {
        animator.SetTrigger("morrer");
        patrol.enabled = false;

        yield return new WaitForSeconds(1.2f);

        SceneManager.LoadScene("Vitoria");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerTiro"))
        {
            TomarDano(1);
            Destroy(collision.gameObject);
        }
    }
}
