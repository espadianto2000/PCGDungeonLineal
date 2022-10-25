using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class statsEnemigo2 : MonoBehaviour
{
    public float vidaMax;
    public float vida;
    public float velocidad;
    public int danoMelee;
    public int danoRango;
    public float velocidadAtaque;
    public float velocidadProyectil;
    public float rango;
    public bool vulnerable = true;
    public GameObject corazon;
    public GameObject medioCorazon;
    public GameObject damageInd;
    // Start is called before the first frame update
    void Start()
    {
        vida = vidaMax;
    }

    // Update is called once per frame
    void Update()
    {
        if (vida <= 0)
        {
            transform.parent.GetComponent<updateCam>().contadorEnemigos -= 1;
            int numero = Random.Range(0, 100);
            if (numero >= 90 && numero <= 95)
            {
                GameObject obj = Instantiate(medioCorazon, transform.position, Quaternion.identity);
                obj.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-2f, 2f), 2.5f, Random.Range(-2f, 2f));
            }
            else if (numero > 95)
            {
                GameObject obj = Instantiate(corazon, transform.position, Quaternion.identity);
                obj.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-2f, 2f), 2.5f, Random.Range(-2f, 2f));
            }
            Destroy(gameObject);
        }
    }
    public void recibirDano(float dano, Vector3 player, float knockbackMelee, int tipoAtaque)
    {
        //vulnerable = false;
        Vector3 direccion = transform.position - player;
        direccion.y = 0;
        if (tipoAtaque == 0)
        {
            transform.GetComponent<EnemyController2>().knocks(direccion, knockbackMelee, tipoAtaque);
        }
        vida = vida - dano;
        GameObject ind = Instantiate(damageInd, transform.position, Quaternion.identity);
        ind.GetComponent<DamagePopup>().setDamageValue(dano);
    }
    public void curarDano(float dano)
    {
        vida = vida + dano;
        GameObject ind = Instantiate(damageInd, transform.position, Quaternion.identity);
        ind.GetComponent<DamagePopup>().setDamageValue(dano);
        ind.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.green;
    }
}
