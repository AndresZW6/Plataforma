using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControladorUI : MonoBehaviour
{
    //Declaramos e iniciamos la instancia del controladorUI
    public static ControladorUI instancia;

    private void Awake()
    {
        instancia = this;

        //iniciar el FadeFromBlack para poder visualizar el juego
        FadeFromBlack();
    }

    //Variables para manipular la imagen en negro
    public Image FadeScreen;
    public bool isFadingToBlack, isFadingFromBlack;
    public float Vfade = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Si se esta desvaneciendo a negro
        if(isFadingToBlack)
        {
            //Operacion para manipular el alpha de 0 a 100
            FadeScreen.color = new Color(FadeScreen.color.r, FadeScreen.color.g, FadeScreen.color.b, 
            Mathf.MoveTowards(FadeScreen.color.a, 1f, Vfade * Time.deltaTime)
            );
        }

        //Si se esta desvaneciendo desde el negro
        if(isFadingFromBlack)
        {
            //Operacion para manipular el alpha de 100 a 0
            FadeScreen.color = new Color(FadeScreen.color.r, FadeScreen.color.g, FadeScreen.color.b, 
            Mathf.MoveTowards(FadeScreen.color.a, 0f, Vfade * Time.deltaTime)
            );
        }
    }

    //Funcion para desvanecer a negro con sus dos condiciones correspondientes
    public void FadeToBlack()
    {
        //Desvaneciendoce a negro es verdadero
        isFadingToBlack = true;
        //Desvaneciendoce desde el negro es falso
        isFadingFromBlack = false;
    }

    //Funcion para desvanecer desde el negro con sus dos condiciones correspondientes
    public void FadeFromBlack()
    {
        //Desvaneciendoce a negro es falso
        isFadingToBlack = false;
        //Desvaneciendoce desde el negro es verdadero
        isFadingFromBlack = true;
    }
}
