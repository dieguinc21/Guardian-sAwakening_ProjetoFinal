using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform alvo; // normalmente o player
    public Vector3 offset; // opcional: distância entre câmera e player
    public float suavizacao = 0.125f; // suaviza o movimento

    private void LateUpdate()
    {
        if (alvo == null) return;

        Vector3 destinoDesejado = alvo.position + offset;
        Vector3 destinoSuavizado = Vector3.Lerp(transform.position, destinoDesejado, suavizacao);
        transform.position = new Vector3(destinoSuavizado.x, destinoSuavizado.y, transform.position.z);
    }
}