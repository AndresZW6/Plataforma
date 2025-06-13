using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraControl : MonoBehaviour
{   
    //Declaramos variable de objetivo a seguir con la camara
    private Transform Objetivo;
    //Declaramos variable para mantener distancia sobre el objetivo
    private Vector3 offset;
    //Declaramos variable de velocidad de camara (movimiento mas natural)
    public float CamVel;
    // Start is called before the first frame update
    void Start()
    {
        //Buscamos el objetivo dentro de la escena de juego
        Objetivo = FindObjectOfType<PlayerControl>().transform;
        //Distancia entre el objetivo y la camara
        offset = transform.position;

        //ControladorNivel.instancia.prueba = 4;
    }

    // Update is called once per frame
    void Update()
    {
        //Vector de movimiento de la camara
        transform.position = Vector3.Lerp(transform.position, Objetivo.position + offset, CamVel * Time.deltaTime);
        //Condicion si caemos del piso
        if(transform.position.y < offset.y)
        {
            transform.position = new Vector3(transform.position.x, offset.y, transform.position.z);
        }
    }  

    public void AnclarObjetivo()
    {
        transform.position = Objetivo.position + offset;
    }
}
