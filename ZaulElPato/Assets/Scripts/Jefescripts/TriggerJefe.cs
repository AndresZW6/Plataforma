using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerJefe : MonoBehaviour
{
    public GameObject JefeActivacion;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            JefeActivacion.SetActive(true);

            gameObject.SetActive(false);
        }
    }

}
