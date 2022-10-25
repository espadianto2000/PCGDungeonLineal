using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class statsRegalo : MonoBehaviour
{
    public int cambiarVidaMax = 0;
    public int cambiarVida = 0;
    public int cambiarVelocidad = 0;
    public int cambiarDanoMelee = 0;
    public int cambiarDanoRango = 0;
    public int cambiarVelocidadAtaqueRango = 0;
    public int cambiarCooldownMelee = 0;
    public int cambiarTamanoEspada = 0;
    public int cambiarRangoDistancia = 0;
    public int cambiarKnockbackMelee = 0;
    public int recibirDano = 0;

    public int puntaje = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void pseudoStart(int p = 0)
    {
        //pseudo modelo
        int[] arr = new int[11];
        if (p != 0)
        {
            puntaje = p;
        }
        else
        {
            puntaje = Mathf.RoundToInt(Random.Range(1, 300) % 3) + 1;
        }
        for (int i = 0; i < puntaje; i++)
        {
            switch (Mathf.RoundToInt(Random.Range(0f, 200f) % 2))
            {
                case 0:
                    arr[Mathf.RoundToInt(Random.Range(0f, 1000f) % 10)]++;
                    break;
                case 1:
                    arr[Mathf.RoundToInt(Random.Range(0f, 1000f) % 10)] += 2;
                    int rand = 1;
                    while (rand == 1)
                    {
                        rand = Mathf.RoundToInt(Random.Range(0f, 1100f) % 11);
                    }
                    arr[rand] -= 1;
                    break;
            }
        }
            
        cambiarVidaMax = arr[0];
        cambiarVida = arr[1];
        cambiarVelocidad = arr[2];
        cambiarDanoMelee = arr[3];
        cambiarDanoRango = arr[4];
        cambiarVelocidadAtaqueRango = arr[5];
        cambiarCooldownMelee = arr[6];
        cambiarTamanoEspada = arr[7];
        cambiarRangoDistancia = arr[8];
        cambiarKnockbackMelee = arr[9];
        recibirDano = arr[10];
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
            if (cambiarVidaMax != 0)
            {
                other.GetComponent<statsJugador>().CambiarVidaMax(2 * cambiarVidaMax);
            }
            if (cambiarVida != 0)
            {
                other.GetComponent<statsJugador>().cambiarVida(cambiarVida);
            }
            if (cambiarVelocidad != 0)
            {
                other.GetComponent<statsJugador>().cambiarVelocidad(0.5f * cambiarVelocidad, false);
            }
            if (cambiarDanoMelee != 0)
            {
                other.GetComponent<statsJugador>().cambiarDanoMelee(1.5f * cambiarDanoMelee, false);
            }
            if (cambiarDanoRango != 0)
            {
                other.GetComponent<statsJugador>().cambiarDanoRango(0.5f * cambiarDanoRango, false);
            }
            if (cambiarVelocidadAtaqueRango != 0)
            {
                other.GetComponent<statsJugador>().cambiarVelocidadAtaqueRango(0.1f * cambiarVelocidadAtaqueRango, false);
            }
            if (cambiarCooldownMelee != 0)
            {
                other.GetComponent<statsJugador>().cambiarCooldownMelee(0.5f * -cambiarCooldownMelee, false);
            }
            if (cambiarTamanoEspada != 0)
            {
                other.GetComponent<statsJugador>().cambiarTamanoEspada(0.5f * cambiarTamanoEspada, false);
            }
            if (cambiarRangoDistancia != 0)
            {
                other.GetComponent<statsJugador>().cambiarRangoDistancia(2 * cambiarRangoDistancia, true);
            }
            if (cambiarKnockbackMelee != 0)
            {
                other.GetComponent<statsJugador>().cambiarKnockbackMelee(0.1f * cambiarKnockbackMelee, false);
            }
            if (recibirDano < 0)
            {
                other.GetComponent<statsJugador>().recibirDano(-2 * recibirDano);
            }
            Destroy(transform.gameObject);
        }
    }
}
