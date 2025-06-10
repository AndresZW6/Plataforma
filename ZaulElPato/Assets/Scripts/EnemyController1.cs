using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController1 : MonoBehaviour
{
    //Enemigo rutina entre dos puntos

    //Variable velocidad de movimiento
    public float VMovimiento;
    //Variable n cantidad de puntos de patrullaje
    public Transform[] PuntosPatrullaje;
    //Variable para guardar la cantidad de puntos de patrullaje
    private int PuntosActuales;

    private Vector3 DireccionMovimiento;

    public Rigidbody ERB;

    private float yCantidad;



    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform pp in PuntosPatrullaje)
        {
            pp.parent = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        yCantidad = ERB.velocity.y;

        DireccionMovimiento = PuntosPatrullaje[PuntosActuales].position - transform.position;

        DireccionMovimiento.y = 0f;
        DireccionMovimiento.Normalize();

        ERB.velocity = DireccionMovimiento * VMovimiento;
        ERB.velocity = new Vector3(ERB.velocity.x, yCantidad, ERB.velocity.z);

        if (Vector3.Distance(transform.position, PuntosPatrullaje[PuntosActuales].position) <= 0.1f)
        {
            PuntoSiguiente();
        }
    }

    public void PuntoSiguiente()
    {
        PuntosActuales ++;

        if(PuntosActuales >= PuntosPatrullaje.Length)
        {
            PuntosActuales = 0;
        }
    }
}
