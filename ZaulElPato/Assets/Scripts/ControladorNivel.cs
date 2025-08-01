using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorNivel : MonoBehaviour
{
    //Variable para poder utilizar el script dentro de otros scripts
    public static ControladorNivel instancia;

    //Acciones que se generan antes del void start
    private void Awake()
    {
        instancia = this;
    }
    //variable de tiempo de espera antes de reapacer
    public float TiempoAntesRespawn;
    //Variable de condicion que me indica cuando estoy reaparecioendo
    public bool Respawneando;

    //Mandamos a llamar al script del control de jugador
    private PlayerControl player;

    public Vector3 puntoRespawn;

    private CamaraControl camara;

    public int ColectAct;

    // Start is called before the first frame update
    void Start()
    {
        //Mandar a llamar el vector del contro de jugador
        player = FindObjectOfType<PlayerControl>();

        //Generamos el punto de reapacion
        puntoRespawn = player.transform.position + Vector3.up;

        camara = FindObjectOfType<CamaraControl>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Funcion para reapacer
    public void Respawn()
    {
        if(!Respawneando)
        {
            Respawneando = true;

            StartCoroutine(TiempoRespawn());
        }
    }

    //Corutina para realizar la reaparicion
    public IEnumerator TiempoRespawn()
    {
        //El jugador se desactiva
        player.gameObject.SetActive(false);

        //Mandamos a llamar al script ControladorUI para desvanecer a negro
        ControladorUI.instancia.FadeToBlack();

        //Comienza la cuenta regresiva
        yield return new WaitForSeconds(TiempoAntesRespawn);

        //Una vez finalice el TiempoAntesRespawn la posicion del jugador va a ser igual a la posicion del puntoRespawn
        //y se activa el jugador
        player.transform.position = puntoRespawn;

        camara.AnclarObjetivo();

        player.gameObject.SetActive(true);

        //Respawneando regresa a ser falso
        Respawneando = false;

        //Mandamos a llamar al script ControladorUI para desvanecer desde el negro
        ControladorUI.instancia.FadeFromBlack();

        VidaJugador.instancia.CantidadVida();

    }

    public void GetColect()
    {
        ColectAct++;

        ControladorUI.instancia.ColeccionableText.text = ColectAct.ToString();
    }
    
}
