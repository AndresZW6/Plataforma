using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public GameObject efecto;

    public Transform PuntoEfecto;
    
    public Animator animCheck;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (ControladorNivel.instancia.puntoRespawn != transform.position)
            {

            ControladorNivel.instancia.puntoRespawn = transform.position;

            if(efecto != null)
            {
            Instantiate(efecto, PuntoEfecto.position, Quaternion.identity);
            }

            CheckPoint[] TodosCheckPoint = FindObjectsOfType<CheckPoint>();
            foreach(CheckPoint checkpoint in TodosCheckPoint)
            {
                checkpoint.animCheck.SetBool("Activo", false);
            }

            
            animCheck.SetBool("Activo", true);
            }
        }
    }
}
