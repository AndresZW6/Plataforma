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

    public PlayerControl Eljugador;

    private float yCantidad;

    public float EsperaAntesDestruir;
    public float AplastarV;
    private float ContadorMuerte;





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
        if(ContadorMuerte > 0)
        {
            ContadorMuerte -= Time.deltaTime;
            ERB.velocity = new Vector3(0f, ERB.velocity.y, 0f);

            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1.3f, 0.05f, 1.3f), AplastarV * Time.deltaTime);

            if(ContadorMuerte <=0)
            {
                Destroy(gameObject);
            }
        }

        else
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
    }

    public void PuntoSiguiente()
    {
        PuntosActuales ++;

        if(PuntosActuales >= PuntosPatrullaje.Length)
        {
            PuntosActuales = 0;
        }
    }

    private void OnCollisionStay(Collision contacto)
    {
        if(contacto.gameObject.tag == "Player" && ContadorMuerte == 0)
        {
            VidaJugador.instancia.DanoJugador();
        }

    }

    private void OnTriggerEnter(Collider otro)
    {
        if(otro.tag == "Player")
        {        
            ContadorMuerte = EsperaAntesDestruir;

            Eljugador.Rebote();
        }
    }


}
