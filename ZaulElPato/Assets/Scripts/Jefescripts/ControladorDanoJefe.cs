using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorDanoJefe : MonoBehaviour
{
    public ControladorJefe JefeCon;

    private PlayerControl jugador;

    public bool DanadoJefe;

    private void Start()
    {
        jugador = FindObjectOfType<PlayerControl>();

        DanadoJefe = false;
    }

    private void Update()
    {
        if (DanadoJefe == false)
        {
         JefeCon.AtaqueJefe();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            DanadoJefe = true;

            JefeCon.DanoJefe();

            JefeCon.IdleJefe();

            jugador.Rebote();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            DanadoJefe = false;
        }
    }
}
