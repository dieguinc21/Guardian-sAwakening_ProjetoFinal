using UnityEngine;

public class PlataformaLateral : MonoBehaviour
{
    public Transform pontoEsquerda;
    public Transform pontoDireita;
    public float velocidade = 2f;

    private Vector3 alvo;

    void Start()
    {
        alvo = pontoDireita.position; // começa indo para a direita
    }

    void Update()
    {
        // Move a plataforma até o alvo
        transform.position = Vector3.MoveTowards(transform.position, alvo, velocidade * Time.deltaTime);

        // Troca o alvo quando chega
        if (Vector3.Distance(transform.position, alvo) < 0.05f)
        {
            if (alvo == pontoDireita.position)
                alvo = pontoEsquerda.position;
            else
                alvo = pontoDireita.position;
        }
    }

    // --- LÓGICA QUE GRUDA O PLAYER NA PLATAFORMA ---
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            collision.transform.SetParent(transform); // player vira filho da plataforma
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            collision.transform.SetParent(null); // volta ao normal
        }
    }
}
