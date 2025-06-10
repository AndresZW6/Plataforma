using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInstaKill : MonoBehaviour
{
    //Interacci�n al entrar al trigger
    public void OnTriggerEnter(Collider other)
    {
        //Si otro tag es el player
        if(other.tag == "Player")
        {
            //other.gameObject.GetComponent<CharacterController>().Move(Vector3.up - other.transform.position);
            ControladorNivel.instancia.Respawn();
        }
    }
}
