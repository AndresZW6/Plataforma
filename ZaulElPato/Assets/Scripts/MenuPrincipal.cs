using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    //Declaramos variables publicas del tipo CanvasGroup
    public CanvasGroup MenuCanvas;
    public CanvasGroup SUBMenuOptions;

    //Acciones que se ejecturan al inicio de la ejecucion
    void Start()
    {
        SUBMenuOptions.gameObject.SetActive(false);
        MenuCanvas.gameObject.SetActive(true);
    }

    public void Play()
    {
        SceneManager.LoadScene("Nivel1");
    }

    public void Options()
    {
        SUBMenuOptions.gameObject.SetActive(true);
        MenuCanvas.gameObject.SetActive(false);
    }

    public void Return()
    {
        //Desactive el submenu de opciones
        SUBMenuOptions.gameObject.SetActive(false);
        //Activar el menu principal
        MenuCanvas.gameObject.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
        Debug.Log("Cerrando el juego");
    }
}
