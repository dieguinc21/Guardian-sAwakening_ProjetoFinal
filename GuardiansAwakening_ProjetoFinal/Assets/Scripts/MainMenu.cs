using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("Cutcene1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Voltar()
    {
        SceneManager.LoadSceneAsync("MainMenu");
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
        SceneManager.LoadSceneAsync("Cutcene2");
    }
    
    public void ProximoC2()
    {
        SceneManager.LoadSceneAsync("Fase1");
    }
    
}
