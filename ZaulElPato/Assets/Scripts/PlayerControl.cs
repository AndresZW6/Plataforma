using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    //Declarando variable de velocidad de dezplazamiento (1)
    public float VDesplazamiento;

    //Declarando character controller (Fisicas) (2)
    public CharacterController CharCon;

    //Variable camara (3)
    private CamaraControl camara;

    private Vector3 CantidadMovimiento;

    //Variables para gravedad y salto (4)

    public float FuerzaSalto, EscalaGravedad;
    private float yCantidad;

    //Variable de rotacion (6)
    public float VRotacion = 10f;
        

    // Start is called before the first frame update
    void Start()
    {
        //Detección script camara
        camara = FindObjectOfType<CamaraControl>();
    }

    //Condicion de Gravedad (4)
    private void FixedUpdate()
    {
        if (!CharCon.isGrounded)
        {
            CantidadMovimiento.y = CantidadMovimiento.y + (Physics.gravity.y * EscalaGravedad * Time.fixedDeltaTime);
        }
        else
        {
            CantidadMovimiento.y = Physics.gravity.y * EscalaGravedad * Time.deltaTime;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Igualando cantidad de salto con la cantidad de movimiento
        yCantidad = CantidadMovimiento.y;

        //Desplazamiento independiente a la camara
        CantidadMovimiento = (camara.transform.forward * Input.GetAxisRaw("Vertical")) + (camara.transform.right * Input.GetAxisRaw("Horizontal"));

        //la siguiente linea corrige el movimiento con la rotacion
        CantidadMovimiento.y = 0f;

        CantidadMovimiento = CantidadMovimiento.normalized;


        //rotacion  de la vista del personaje en direccion del desplazamiento
        if(CantidadMovimiento.magnitude > 0.1f)
        {
            if(CantidadMovimiento != Vector3.zero)
            {
                Quaternion nuevaRotacion = Quaternion.LookRotation(CantidadMovimiento);

                transform.rotation = Quaternion.Slerp(transform.rotation, nuevaRotacion, VRotacion * Time.deltaTime);
            }
        }


        // Salto personaje si esta en el piso (4)

        CantidadMovimiento.y = yCantidad;

        if (CharCon.isGrounded)
        {
            if(Input.GetButtonDown("Jump"))
            {
                CantidadMovimiento.y = FuerzaSalto;
            }
        }

        CharCon.Move(new Vector3(CantidadMovimiento.x * VDesplazamiento, CantidadMovimiento.y, CantidadMovimiento.z * VDesplazamiento) * Time.deltaTime);
        //Control de desplazamiento (1) Vector3(x,y,z)
        //transform.position = new Vector3(transform.position.x + (Input.GetAxisRaw("Horizontal") * VDesplazamiento * Time.deltaTime), transform.position.y, transform.position.z + (Input.GetAxisRaw("Vertical")* VDesplazamiento * Time.deltaTime));

        //Control de movimiento con Character Controller (2)
        //CharCon.Move(new Vector3(Input.GetAxisRaw("Horizontal") * VDesplazamiento, 0f, Input.GetAxisRaw("Vertical") * VDesplazamiento) * Time.deltaTime);

    }
}
