using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Alvo")]
    public Transform alvo; // normalmente o player
    public Vector3 offset; // distância entre câmera e player
    public float suavizacao = 0.125f;

    [Header("Limites da Câmera")]
    public bool limitarMovimento = true;
    public Vector2 limiteMinimo; // canto inferior esquerdo
    public Vector2 limiteMaximo; // canto superior direito

    private void LateUpdate()
    {
        if (alvo == null) return;

        // posição desejada da câmera
        Vector3 destinoDesejado = alvo.position + offset;
        Vector3 destinoSuavizado = Vector3.Lerp(transform.position, destinoDesejado, suavizacao);

        // aplica os limites se estiver ativado
        if (limitarMovimento)
        {
            float clampedX = Mathf.Clamp(destinoSuavizado.x, limiteMinimo.x, limiteMaximo.x);
            float clampedY = Mathf.Clamp(destinoSuavizado.y, limiteMinimo.y, limiteMaximo.y);
            transform.position = new Vector3(clampedX, clampedY, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(destinoSuavizado.x, destinoSuavizado.y, transform.position.z);
        }
    }
}