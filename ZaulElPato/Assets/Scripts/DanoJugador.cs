using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanoJugador : MonoBehaviour
{
    //Interaccion con colisiones para detectar da�o
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            VidaJugador.instancia.DanoJugador();
        }
    }
}
