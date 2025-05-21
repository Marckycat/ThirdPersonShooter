using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void EscenaJuego()
    {
        SceneManager.LoadScene("GameTPS");
    }

    public void Salir()
    {
        Application.Quit();
    }
    // Reinicia el nivel actual
    public void ReiniciarNivel()
    {
        // Carga la escena activa nuevamente
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("GameTPS");
    }
}
