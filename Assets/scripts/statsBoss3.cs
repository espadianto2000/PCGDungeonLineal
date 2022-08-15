using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class statsBoss3 : MonoBehaviour
{
    public float vidaMax;
    public float vida;
    public float velocidad;
    public int danoMelee;
    public int danoRango;
    public float rango;
    public bool vulnerable = true;
    public bool muerto;
    public float ataqueDistanciaTiempo;
    public float spawnEnemigosTiempo;
    public int maxEnemigosSpawneados;
    public int maxProyectiles;
    public float velocidadProyectil;

    public GameObject damageInd;
    // Start is called before the first frame update
    void Start()
    {
        vida = vidaMax;
    }

    // Update is called once per frame
    void Update()
    {
        if (vida <= 0 && !muerto)
        {
            muerto = true;
            GetComponent<bossController3>().anim.SetTrigger("morir");
            GameObject.Find("AudioManager").GetComponent<AudioManager>().activarWin();
            destruir();
        }
    }
    public void recibirDano(float dano, Vector3 player, int tipoAtaque)
    {
        vida = vida - dano;
        GameObject ind = Instantiate(damageInd, transform.position, Quaternion.identity);
        ind.GetComponent<DamagePopup>().setDamageValue(dano);
    }
    public void destruir()
    {
        transform.parent.GetComponentInChildren<updateCam>().contadorEnemigos -= 1;
        Invoke("eliminar", 4f);
    }
    private void eliminar()
    {
        Destroy(gameObject);
    }
}
