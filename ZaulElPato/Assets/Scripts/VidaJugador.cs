using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaJugador : MonoBehaviour
{
    //Instancia de la vida para utilizarla en otros scripts
    public static VidaJugador instancia;

    private void Awake()
    {
        instancia = this;
    }

    //Variables para manipular la vida del jugador
    private int VidaActual;
    public int VidaMax;

    //Variables al recibir daño tenemos un lapso de tiempo de invencibilidad
    public float TiempoInvensible = 1f;
    private float ContadorInvencible;

    public GameObject[] ModelosList;
    private float parpadeo;
    public float TiempoParpadeo = 0.1f;


     void Start()
    {
        CantidadVida();
    }

    void Update ()
    {
        //Condicion para no tener invencibilidad
        if(ContadorInvencible > 0)
        {
            ContadorInvencible -= Time.deltaTime;

            parpadeo -= Time.deltaTime;

            if(parpadeo <= 0)
            {
                parpadeo = TiempoParpadeo;
                foreach(GameObject seccion in ModelosList)
                {
                    seccion.SetActive(!seccion.activeSelf);
                }
            }

            if (ContadorInvencible <= 0)
            {
                foreach (GameObject seccion in ModelosList)
                {
                    seccion.SetActive(true);
                }
            }
        }
    }

    //Funcion daño al jugador
    public void DanoJugador()
    {  
        //Condicion para activar invencibilidad al recibir daño
        if(ContadorInvencible <=0)
        {
        ContadorInvencible = TiempoInvensible;

        VidaActual --;

        if (VidaActual <= 0)
        {
            ControladorNivel.instancia.Respawn();
        }

        //comentar
        ControladorUI.instancia.ActualizarVida(VidaActual);

        }
    }

    public void CantidadVida()
    {
        VidaActual = VidaMax;

        //Comentar
        ControladorUI.instancia.ActualizarVida(VidaActual);
    }
}
