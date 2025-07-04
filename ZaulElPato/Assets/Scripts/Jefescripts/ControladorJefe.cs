using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControladorJefe : MonoBehaviour
{
    public int VidaMax;
    private int VidaActual;

    public Slider SliderVida;

    public Animator animjefe;

    public float TiempoAntesAtaque = 3f;

    // Start is called before the first frame update
    void Start()
    {
        VidaActual = VidaMax;

        SliderVida.maxValue = VidaMax;
        SliderVida.value = VidaActual;
    }


    public void DanoJefe()
    {
        VidaActual --;
        if(VidaActual <= 0)
        {
            gameObject.SetActive(false);

            VidaActual = 0;
        }

        SliderVida.value = VidaActual;
    }

    public IEnumerator TiempoAtaque()
    {
        animjefe.SetBool("isAttack", true); 

        yield return new WaitForSeconds(TiempoAntesAtaque);
    }

    public void AtaqueJefe()
    {
        StartCoroutine(TiempoAtaque());
    }

    public void IdleJefe()
    {
        animjefe.SetBool("isAttack", false);
    }
}
