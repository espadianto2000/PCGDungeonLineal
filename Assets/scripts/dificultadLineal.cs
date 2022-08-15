using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class statsEnem4
{
    public float vidaMax;
    public float velocidad;
    public int danoMelee;
    public float cooldownAtaque;
    public statsEnem4(int nivelDificultad)
    {
        this.vidaMax = 5.25f + (2.75f * nivelDificultad);
        this.velocidad = 1.5f + (0.25f * nivelDificultad);
        this.danoMelee = 1 + (int)Math.Floor(0.34f * nivelDificultad);
        this.cooldownAtaque = 8f - (0.75f * (nivelDificultad >= 9 ? 8 : nivelDificultad));
    }
}
public class statsEnem3
{
    public float vidaMax;
    public float velocidad;
    public int danoMelee;
    public float velocidadCrecimiento;
    public float cooldownCuracion;
    public float curacion;
    public statsEnem3(int nivelDificultad)
    {
        this.vidaMax = 5 + (2.5f * nivelDificultad);
        this.velocidad = 1.5f + (0.25f * nivelDificultad);
        this.danoMelee = 1 + (int)Math.Floor(0.34f * nivelDificultad);
        this.velocidadCrecimiento = 3.5f + (0.5f * nivelDificultad);
        this.cooldownCuracion = 6.75f - (0.75f * nivelDificultad);
        this.curacion = 1.25f + (0.25f * nivelDificultad);
    }
}
public class statsEnem2
{
    public float vidaMax;
    public float velocidad;
    public int danoMelee;
    public int danoRango;
    public float velocidadAtaque;
    public float velocidadProyectil;
    public float rango;
    public statsEnem2(int nivelDificultad)
    {
        this.vidaMax = 5.25f + (2.75f * nivelDificultad);
        this.velocidad = 1.5f + (0.25f * nivelDificultad);
        this.danoMelee = 1 + (int)Math.Floor(0.34f * nivelDificultad);
        this.danoRango = 1 + (int)Math.Floor(0.34f * nivelDificultad);
        this.velocidadAtaque = 6f - (0.5f * (nivelDificultad >= 11 ? 10 : nivelDificultad));
        this.velocidadProyectil = 2 + (0.75f * nivelDificultad);
        this.rango = 0.5f + (0.25f * nivelDificultad);
    }
}
public class statsEnem1
{
    public float vidaMax;
    public float velocidad;
    public int danoMelee;
    public statsEnem1(int nivelDificultad)
    {
        this.vidaMax = 7 + (3 * nivelDificultad);
        this.velocidad = 1.5f + (0.25f * nivelDificultad);
        this.danoMelee = 1 + (int)Math.Floor(0.34f * nivelDificultad);
    }
}
public class stBoss3
{
    public float vidaMax;
    public float velocidad;
    public int danoMelee;
    public int danoRango;
    public float rango;
    public float ataqueDistanciaTiempo;
    public float spawnEnemigosTiempo;
    public int maxEnemigosSpawneados;
    public int maxProyectiles;
    public float velocidadProyectil;
    public stBoss3(int nivelDificultad)
    {
        this.vidaMax = 30 + (30 * nivelDificultad);
        this.velocidad = 2f + (0.75f * nivelDificultad);
        this.danoMelee = 2 + (int)Math.Floor(0.34f * nivelDificultad);
        this.danoRango = 2 + (int)Math.Floor(0.34f * nivelDificultad);
        this.rango = 0.6f + (0.1f * nivelDificultad);
        this.ataqueDistanciaTiempo = 7 - (0.75f * (nivelDificultad >= 9 ? 8 : nivelDificultad));
        this.spawnEnemigosTiempo = 25 - (2.5f * (nivelDificultad >= 7?6:nivelDificultad));
        this.maxEnemigosSpawneados = 2 + nivelDificultad;
        this.maxProyectiles = 2 + nivelDificultad;
        this.velocidadProyectil = 2 + (0.75f * nivelDificultad);
    }
}
public class stBoss2
{
    public float vidaMax;
    public float velocidad;
    public int danoMelee;
    public float velocidadExtra;
    public int rebotesMax;
    public stBoss2(int nivelDificultad)
    {
        this.vidaMax = 30 + (30 * nivelDificultad);
        this.velocidad = 3f + (0.75f * nivelDificultad);
        this.danoMelee = 2 + (int)Math.Floor(0.34f * nivelDificultad);
        this.velocidadExtra = 0.5f + (0.75f * nivelDificultad);
        this.rebotesMax = 10 - (int)Math.Floor(0.75f * nivelDificultad);
    }
}
public class stBoss1
{
    public float vidaMax;
    public float velocidad;
    public int danoMelee;
    public int danoRango;
    public float timerAtaq;
    public float velocidadGiro;
    public stBoss1(int nivelDificultad)
    {
        this.vidaMax = 30 + (30 * nivelDificultad);
        this.velocidad = 0.5f + (0.5f * nivelDificultad);
        this.danoMelee = 2 + (int)Math.Floor(0.34f * nivelDificultad);
        this.danoRango = 2 + (int)Math.Floor(0.34f * nivelDificultad);
        this.timerAtaq = 6 - (0.75f * (nivelDificultad >= 7 ? 6 : nivelDificultad));
        this.velocidadGiro = 50 + (20 * nivelDificultad);
    }
}

public class dificultadLineal : MonoBehaviour
{
    public int nivelDificultad;
    public int numSalas;
    public int numEnemigos;
    public int numObs;
    public int numTrampas;
    public int probPremio;
    public statsEnem1 stEn1;
    public statsEnem1 stEn12;
    public statsEnem2 stEn2;
    public statsEnem2 stEn22;
    public statsEnem3 stEn3;
    public statsEnem3 stEn32;
    public statsEnem4 stEn4;
    public statsEnem4 stEn42;
    public gameManager gm;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        cambiarNivel(1);
    }
    public void cambiarNivel(int nivelNuevo)
    {
        if(nivelNuevo == 1)
        {
            if(gm.usuario==""|| gm.usuario == null)
            {
                gm.identificadorMaq = gm.panelCarga.GetComponent<panelCarga>().nombre.GetComponent<TMP_InputField>().text + "-|-" + DateTime.Now;
            }
            else
            {
                gm.identificadorMaq = gm.usuario + "-|-" + DateTime.Now;
            }
            gm.identificado = true;
            player.GetComponent<statsJugador>().reiniciar();
        }
        nivelDificultad = nivelNuevo;
        probPremio = 20 + (int)Math.Floor(7.5f * (nivelDificultad >= 10 ? 9 : nivelDificultad));
        numSalas = 2 + (int)(Mathf.Round(2f * nivelDificultad));
        numEnemigos = 3 + (int)Math.Floor(0.75f * (nivelDificultad>=8?7:nivelDificultad));
        numObs = 20 + (5 * nivelDificultad >= 5 ? 4 : nivelDificultad);
        numTrampas = 0 + (int)Math.Floor(0.75f * (nivelDificultad >= 8 ? 7 : nivelDificultad));
        stEn1 = new statsEnem1(nivelDificultad);
        stEn12 = new statsEnem1(nivelDificultad+1);
        stEn2 = new statsEnem2(nivelDificultad);
        stEn22 = new statsEnem2(nivelDificultad+1);
        stEn3 = new statsEnem3(nivelDificultad);
        stEn32 = new statsEnem3(nivelDificultad+1);
        stEn4 = new statsEnem4(nivelDificultad);
        stEn42 = new statsEnem4(nivelDificultad+1);
        //stEn1.vidaMax = 7 + (3 * nivelDificultad);
        //stEn1.velocidad = 1.5f + (0.25f * nivelDificultad);
        //stEn1.danoMelee = 1 + (int)Math.Floor(0.34f * nivelDificultad);
        //stEn2.vidaMax = 5.25f + (2.75f * nivelDificultad);
        //stEn2.velocidad = 1.5f + (0.25f * nivelDificultad);
        //stEn2.danoMelee = 1 + (int)Math.Floor(0.34f * nivelDificultad);
        //stEn2.danoRango = 1 + (int)Math.Floor(0.34f * nivelDificultad);
        //stEn2.velocidadAtaque = 6f - (0.5f * (nivelDificultad>=11? 10 : nivelDificultad));
        //stEn2.velocidadProyectil = 2 + (1 * nivelDificultad);
        //stEn2.rango = 0.5f + (0.25f * nivelDificultad);
        //stEn3.vidaMax = 5 + (2.5f * nivelDificultad);
        //stEn3.velocidad = 1.5f + (0.25f * nivelDificultad);
        //stEn3.danoMelee = stEn1.danoMelee = 1 + (int)Math.Floor(0.34f * nivelDificultad);
        //stEn3.velocidadCrecimiento = 3.5f + (0.5f * nivelDificultad);
        //stEn3.cooldownCuracion = 6.75f - (0.75f * nivelDificultad);
        //stEn3.curacion = 1.25f + (0.25f * nivelDificultad);
        //stEn4.vidaMax = 5.25f + (2.75f * nivelDificultad);
        //stEn4.velocidad = 1.5f + (0.25f * nivelDificultad);
        //stEn4.danoMelee = 1 + (int)Math.Floor(0.34f * nivelDificultad);
        //stEn4.cooldownAtaque = 8f - (0.75f * (nivelDificultad >= 9 ? 8 : nivelDificultad));
    }
}
