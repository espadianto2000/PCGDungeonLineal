using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class statsJugador : MonoBehaviour
{
    public int vidaMax;
    public int vida;
    public float velocidad;
    public float danoMelee;
    public float danoRango;
    public float velocidadAtaqueMelee;
    public float velocidadAtaqueRango;
    public float cooldownMelee;
    public float rangoMelee;
    public float rangoRango;
    public float knockbackMelee;

    public charController control;
    public Animator animations;
    public GameObject espada;
    public GameObject contenedorBarraVida;
    public GameObject contenedorContenedoresVida;
    public GameObject ContenedorVida;
    public GameObject cuadradoVida;
    public TextMeshProUGUI velocidadUI;
    public TextMeshProUGUI danoMeleeUI;
    public TextMeshProUGUI danoRangoUI;
    public TextMeshProUGUI velocidadAtaqueMeleeUI;
    public TextMeshProUGUI velocidadAtaqueRangoUI;
    public TextMeshProUGUI cooldownMeleeUI;
    public TextMeshProUGUI rangoMeleeUI;
    public TextMeshProUGUI rangoRangoUI;
    public TextMeshProUGUI knockbackMeleeUI;


    private bool pGracia = false;
    // Start is called before the first frame update
    void Start()
    {
        //control.speed = velocidad;
        iniciar();
    }
    public void reiniciar()
    {
        vidaMax = 8;
        velocidad = 3;
        danoMelee = 3;
        danoRango = 1.5f;
        velocidadAtaqueMelee = 1;
        velocidadAtaqueRango = 1;
        cooldownMelee = 5;
        rangoMelee = 1.5f;
        rangoRango = 0.1f;
        knockbackMelee = 1;
        control.vivo = true;
        control.cont.enabled = false;
        Collider[] cols = GetComponentsInChildren<Collider>();
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        foreach (Collider col in cols)
        {
            col.enabled = true;
        }
        iniciar();
        
    }
    public void iniciar()
    {
        animations.SetFloat("multipleSpeedMelee", velocidadAtaqueMelee);
        float ancho = (rangoMelee + 4.5f) / 6;
        espada.transform.localScale = new Vector3(ancho, rangoMelee, espada.transform.localScale.z);
        vida = vidaMax;
        animations.SetFloat("multipleSpeedThrow", velocidadAtaqueRango);
        pintarVida();
        velocidadUI.text = Math.Round((velocidad / 0.5f) - 1) + "";
        danoMeleeUI.text = Math.Round(danoMelee / 1.5f) + "";
        danoRangoUI.text = Math.Round(danoRango / 0.5f) + "";
        velocidadAtaqueRangoUI.text = Math.Round((velocidadAtaqueRango / 0.1f) - 6) + "";
        cooldownMeleeUI.text = (cooldownMelee + "s");
        rangoMeleeUI.text = Math.Round((rangoMelee / 0.5) - 1f) + "";
        rangoRangoUI.text = Math.Round(Mathf.Log(rangoRango * 10, 2) + 1) + "";
        knockbackMeleeUI.text = (Math.Round(knockbackMelee / 0.1f) - 4) + "";
    }

// Update is called once per frame
    private void FixedUpdate()
    {
        if (pGracia && GetComponent<charController>().vivo)
        {
            if (this.GetComponentInChildren<SkinnedMeshRenderer>().enabled)
            {
                this.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
            }
            else
            {
                this.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
            }
        }
        if(vida <= 0 && GetComponent<charController>().vivo)
        {
            GetComponent<charController>().morir();
            disolverGracia();
        }
    }
    public void CambiarVidaMax(int vidaExtra)
    {
        vidaMax += vidaExtra;
        if (vidaExtra > 0) { vida += vidaExtra; }
        else { if (vida > vidaMax) { vida = vidaMax; } }
        pintarVida();
    }
    public void pintarVida()
    {
        float cont = vidaMax / 2;
        foreach (Transform child in contenedorContenedoresVida.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach (Transform child in contenedorBarraVida.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        for (int i = 0; i < Math.Floor(cont); i++)
        {
            Instantiate(ContenedorVida, contenedorContenedoresVida.transform);
        }
        for (int j = 0; j < vida; j++)
        {
            Instantiate(cuadradoVida, contenedorBarraVida.transform);
        }
    }
    public void cambiarVida(int punt)
    {
        for(int i = 0; i < punt; i++)
        {
            if (vida == vidaMax)
            {
                break;
            }
            else
            {
                vida++;
                Instantiate(cuadradoVida, contenedorBarraVida.transform);
            }
        }
    }
    public void cambiarVelocidad(float velocidadExtra, bool multiplier)
    {
        if (multiplier)
        {
            velocidad = velocidad * velocidadExtra;
        }
        else
        {
            velocidad = velocidad + velocidadExtra;
        }
        if (velocidad < 1) { velocidad = 1; }
        //control.speed = velocidad;
        velocidadUI.text = Math.Round((velocidad / 0.5f) - 1) + "";
    }
    public void cambiarDanoMelee(float danoExtra, bool multiplier)
    {
        if (multiplier)
        {
            danoMelee = danoMelee * danoExtra;
        }
        else
        {
            danoMelee = danoMelee + danoExtra;
        }
        if (danoMelee < 1.5f) { danoMelee = 1.5f; }
        danoMeleeUI.text = Math.Round(danoMelee / 1.5f) + "";
    }
    public void cambiarDanoRango(float danoExtra, bool multiplier)
    {
        if (multiplier)
        {
            danoRango = danoRango * danoExtra;
        }
        else
        {
            danoRango = danoRango + danoExtra;
        }
        if (danoRango < 0.5f) { danoRango = 0.5f; }
        danoRangoUI.text = Math.Round(danoRango / 0.5f) + "";
    }
    public void cambiarVelocidadAtaqueMelee(float velocidadExtra, bool multiplier)
    {
        if (multiplier)
        {
            velocidadAtaqueMelee = velocidadAtaqueMelee * velocidadExtra;
        }
        else
        {
            velocidadAtaqueMelee = velocidadAtaqueMelee + velocidadExtra;
        }
        if (velocidadAtaqueMelee < 0.5f)
        {
            velocidadAtaqueMelee = 0.5f;
        }
        animations.SetFloat("multipleSpeedMelee", velocidadAtaqueMelee);
    }
    public void cambiarVelocidadAtaqueRango(float velocidadExtra, bool multiplier)
    {
        if (multiplier)
        {
            velocidadAtaqueRango = velocidadAtaqueRango * velocidadExtra;
        }
        else
        {
            velocidadAtaqueRango = velocidadAtaqueRango + velocidadExtra;
        }
        if (velocidadAtaqueRango < 0.7f) { velocidadAtaqueRango = 0.7f; }
        animations.SetFloat("multipleSpeedThrow", velocidadAtaqueRango);
        velocidadAtaqueRangoUI.text = Math.Round((velocidadAtaqueRango / 0.1f) -6) + "";
    }
    public void cambiarCooldownMelee(float cooldownExtra, bool multiplier)
    {

        if (multiplier)
        {
            cooldownMelee = cooldownMelee * cooldownExtra;
        }
        else
        {
            cooldownMelee = cooldownMelee + cooldownExtra;
        }
        if (cooldownMelee > 7.5f) { cooldownMelee = 7.5f; }
        if (cooldownMelee < 1) { cooldownMelee = 1; }
        cooldownMeleeUI.text = (cooldownMelee + "s");
    }
    public void cambiarTamañoEspada(float rangoExtra, bool multiplier)
    {
        if (multiplier)
        {
            rangoMelee = rangoMelee * rangoExtra;
        }
        else
        {
            rangoMelee = rangoMelee + rangoExtra;
        }
        if (rangoMelee < 1) { rangoMelee = 1; }
        float ancho = (rangoMelee + 4.5f) / 6;
        espada.transform.localScale = new Vector3(ancho,rangoMelee, espada.transform.localScale.z);
        rangoMeleeUI.text = Math.Round((rangoMelee / 0.5) - 1) + "";
    }
    public void cambiarRangoDistancia(float rangoExtra, bool multiplier)
    {
        if (multiplier)
        {
            rangoRango = rangoRango * rangoExtra;
        }
        else
        {
            rangoRango = rangoRango + rangoExtra;
        }
        if (rangoRango < 0.1f) { rangoRango = 0.1f; }
        rangoRangoUI.text = Math.Round(Mathf.Log(rangoRango * 10, 2) + 1) + "";
        // TODO
        // cambiar tiempo de vida de proyectil
    }
    public void cambiarKnockbackMelee(float knockbackExtra, bool multiplier)
    {
        if (multiplier)
        {
            knockbackMelee = knockbackMelee * knockbackExtra;
        }
        else
        {
            knockbackMelee = knockbackMelee + knockbackExtra;
        }
        if (knockbackMelee < 0.5f) { knockbackMelee = 0.5f; }
        knockbackMeleeUI.text = Math.Round(Math.Round(knockbackMelee / 0.1f) - 4) + "";
    }
    public void recibirDano(int dano)
    {
        if (!pGracia)
        {
            if (control.salaActual != null)
            {
                control.salaActual.danoRecibidoEnSala += dano;
                control.danoNivel += dano;
            }
            pGracia = true;
            vida -= dano;
            Invoke("disolverGracia", 1f);
            for(int i = 0; i < dano; i++)
            {
                if (contenedorBarraVida.transform.childCount-(i+1) >= 0)
                {
                    Destroy(contenedorBarraVida.transform.GetChild(contenedorBarraVida.transform.childCount-(i+1)).gameObject);
                }
            }
        }
    }
    private void disolverGracia()
    {
        pGracia = false;
        this.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
    }
}
