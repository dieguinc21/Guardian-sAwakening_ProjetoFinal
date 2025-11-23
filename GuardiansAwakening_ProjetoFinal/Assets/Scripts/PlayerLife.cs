using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    public int vidas = 3;

    public void TakeDamage(int dano)
    {
        vidas -= dano;

        // Caso futuras animações de dano
        Debug.Log("Player levou dano! Vidas restantes: " + vidas);

        if (vidas <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
