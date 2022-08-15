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
            Instantiate(cadaverBoss2,transform.position,Quaternion.Euler(-90,0,0));
            transform.parent.GetComponentInChildren<updateCam>().contadorEnemigos -= 1;
            GameObject.Find("AudioManager").GetComponent<AudioManager>().activarWin();
            Destroy(gameObject);
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
}
