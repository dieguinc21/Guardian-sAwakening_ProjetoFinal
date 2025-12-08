using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public void BotãoJogar()
    {
        SceneManager.LoadScene("Cutcene1");
    }
    
    public void PlayGame()
    {
        SceneManager.LoadScene("Fase1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Voltar()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void SomOn()
    {
        AudioListener.volume = 1f;
        Debug.Log("Sons ligados");
    }

    public void SomOff()
    {
        AudioListener.volume = 0f;
        Debug.Log("Sons desligados");
    }

    public void ProximoC1()
    {
        SceneManager.LoadScene("Cutcene2");
    }
}
