using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void Jogar()
    {
        SceneManager.LoadScene("Scenes/Fase1");
    }

    public void Ajustes()
    {
        SceneManager.LoadScene("Scenes/Configuracoes");
    }

    public void Creditos()
    {
        SceneManager.LoadScene("Scenes/Creditos");
    }
}