using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    //Declaramos objetos para los disparadores/colisionadores
    public GameObject cubo, esfera, cilindro,pressF;
     
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
        
    //Variable de animacion (7)
    public Animator anim;

    //Variable de ataque (8)
    public bool atacando;

    //Si tenemos un combo
    public int combo;

    // Start is called before the first frame update
    void Start()
    {
        //Detección script camara
        camara = FindObjectOfType<CamaraControl>();

        cubo.SetActive(false);
        esfera.SetActive(true);
        cilindro.SetActive(false);
        pressF.SetActive(false);
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

        float MovVel = new Vector3 (CantidadMovimiento.x, 0f, CantidadMovimiento.z).magnitude + VDesplazamiento;

        anim.SetFloat("velocidad", MovVel);
        anim.SetBool("isGrounded", CharCon.isGrounded);
        anim.SetFloat("yVelocidad", CantidadMovimiento.y);

        //si pulso el click izquierdo y el personaje se encuentra en el piso ataques en tierra (8)
        //if (Input.GetMouseButtonDown(0) && CharCon.isGrounded)
        //{
            //atacando es verdadero
            //atacando = true;
            AtaqueCombo();
            //si atacando es verdadero
            //if(atacando == true)
            //{
            //    anim.SetBool("isAttack", true);
            //}
        //}
        //else{
           // DesactivarAtaque();
        //}
    }

    //Sistema de interacciones al entrar al disparador/colisionador
    //private void OnTriggerEnter(Collider other)
    //{
        //if(other.name == "TriggerCubo")
        //{
            //Debug.Log("Estas entrando al Cubo");
            //pressF.SetActive(true);          
        //}
        //if(other.name == "TriggerEsfera")
        //{
            //Debug.Log("Estas entrando a la esfera");
            //cubo.SetActive(true);
        //}
        //if(other.name == "TriggerCilindro")
        //{
            //Debug.Log("Estas entrando al cilindro");
            //SceneManager.LoadScene("Menu");

        //}
    //}

    //Sistema de interacciones al salir del disparador/colisionador Ejemplo
    //private void OnTriggerExit(Collider other)
    //{
       // if(other.name == "TriggerCubo")
        //{
           // Debug.Log("Estas saliendo del Cubo");
            //pressF.SetActive(false);
        //}
        //if(other.name == "TriggerEsfera")
        //{
            //Debug.Log("Estas saliendo de la esfera");
            //esfera.SetActive(false);
        //}
        //if(other.name == "TriggerCilindro")
        //{
            //Debug.Log("Estas saliendo del cilindro");
        //}
    //}

    //Sistema de interacciones al estar dentro del disparador/colisionador
    //private void OnTriggerStay(Collider other)
    //{
        //if(other.name == "TriggerCubo")
        //{
            //Debug.Log("Estas dentro del Cubo");
            //if(Input.GetKey(KeyCode.F))
            //{
                //cubo.SetActive(false);
                //cilindro.SetActive(true);
                //pressF.SetActive(false);
            //}
        //}
        //if(other.name == "TriggerEsfera")
        //{
            //Debug.Log("Estas dentro de la esfera");
        //}
        //if(other.name == "TriggerCilindro")
        //{
            //Debug.Log("Estas dentro del cilindro");
        //}
    //}

    public void DesactivarAtaque()
    {
        atacando = false;
        combo = 0;
        anim.SetBool("isAttack", false);       
    }

    public void AtaqueCombo()
    {
        if(Input.GetMouseButtonDown(0) && !atacando)
        {
            atacando = true;
            anim.SetBool("isAttack", true); 
            anim.SetTrigger("" + combo);
        }
    }

    public void EmpezarCombo()
    {
        atacando = false;
        if(combo < 3)
        {
            combo++;
        }
    }
}

