using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movimentação")]
    public float speed = 5f;
    public float jumpForce = 5f;

    [Header("Limites de Tela")]
    public Camera cam; // câmera usada para limitar o movimento
    public float margem = 0.5f; // distância mínima das bordas

    [Header("Limite de Queda")]
    public float limiteQuedaY = -10f; // se o player cair abaixo disso, ele será resetado
    public Vector3 pontoInicial = Vector3.zero; // posição de respawn

    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (cam == null)
            cam = Camera.main;

        // Define o ponto inicial (caso o player caia)
        pontoInicial = transform.position;
    }

    void Update()
    {
        // Movimento horizontal
        float move = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(move * speed, rb.linearVelocity.y);

        // Pular
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        // Travar player dentro da área visível da câmera
        LimitarPosicaoNaTela();

        // Evitar queda infinita
        VerificarQueda();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = false;
    }

    private void LimitarPosicaoNaTela()
    {
        if (cam == null) return;

        // Pega os limites da tela visível da câmera
        Vector3 limiteInferiorEsquerdo = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        Vector3 limiteSuperiorDireito = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));

        // Clampa (limita) a posição do player para ficar dentro da tela
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, limiteInferiorEsquerdo.x + margem, limiteSuperiorDireito.x - margem);
        pos.y = Mathf.Clamp(pos.y, limiteInferiorEsquerdo.y + margem, limiteSuperiorDireito.y - margem);
        transform.position = pos;
    }

    private void VerificarQueda()
    {
        if (transform.position.y < limiteQuedaY)
        {
            // Reseta o player no ponto inicial e zera a velocidade
            transform.position = pontoInicial;
            rb.linearVelocity = Vector2.zero;
        }
    }
}