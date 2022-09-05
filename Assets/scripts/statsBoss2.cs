using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class statsBoss2 : MonoBehaviour
{
    public float vidaMax;
    public float vida;
    public float velocidad;
    public float velocidadExtra;
    public int danoMelee;
    public bool vulnerable = true;
    public bool muerto;
    public int rebotesMax;

    public GameObject damageInd;
    public GameObject cadaverBoss2;
    private GameObject cadaver;
    void Start()
    {
        vida = vidaMax;
    }
    void Update()
    {
        if (vida <= 0 && !muerto)
        {
            muerto = true;
            //matarBoss
            cadaver = Instantiate(cadaverBoss2,transform.position,Quaternion.Euler(-90,0,0));
            transform.GetComponent<MeshRenderer>().enabled = false;
            transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().enabled = false;
            transform.GetComponent<bossController2>().enabled = false;
            GetComponents<Collider>()[1].enabled = false;
            GetComponents<Collider>()[0].enabled = false;
            //transform.parent.GetComponentInChildren<updateCam>().contadorEnemigos -= 1;
            GameObject.Find("AudioManager").GetComponent<AudioManager>().activarWin();
            //Destroy(gameObject);
            Invoke("destruir", 2f);
        }
    }
    public void recibirDano(float dano, Vector3 player, int tipoAtaque)
    {
        //vulnerable = false;
        Vector3 direccion = transform.position - player;
        direccion.y = 0;
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
        Destroy(cadaver);
        Destroy(gameObject);
    }
}
