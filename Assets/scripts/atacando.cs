using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class atacando : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemigo") && player.GetComponentInChildren<Animator>().GetCurrentAnimatorClipInfo(1)[0].clip.name == "Clip1")
        {
            if(other.name.Contains("enemigoV1"))
            {
                if (other.GetComponent<statsEnemigo>().vulnerable == true)
                {
                    //Debug.Log("Se hace daño al enemigo");
                    other.GetComponent<statsEnemigo>().vulnerable = false;
                    other.GetComponent<statsEnemigo>().recibirDano(player.GetComponent<statsJugador>().danoMelee, transform.position, player.GetComponent<statsJugador>().knockbackMelee, 0);
                }
            }else if(other.name.Contains("enemigoV2")){
                if (other.GetComponent<statsEnemigo2>().vulnerable == true)
                {
                    //Debug.Log("Se hace daño al enemigo");
                    other.GetComponent<statsEnemigo2>().vulnerable = false;
                    other.GetComponent<statsEnemigo2>().recibirDano(player.GetComponent<statsJugador>().danoMelee, transform.position, player.GetComponent<statsJugador>().knockbackMelee, 0);
                }
            }
            else if (other.name.Contains("enemigoV3"))
            {
                if (other.GetComponent<statsEnemigo3>().vulnerable == true)
                {
                    //Debug.Log("Se hace daño al enemigo");
                    other.GetComponent<statsEnemigo3>().vulnerable = false;
                    other.GetComponent<statsEnemigo3>().recibirDano(player.GetComponent<statsJugador>().danoMelee, transform.position, player.GetComponent<statsJugador>().knockbackMelee, 0);
                }
            }
            else if (other.name.Contains("enemigoV4"))
            {
                if (other.GetComponent<statsEnemigo4>().vulnerable == true)
                {
                    //Debug.Log("Se hace daño al enemigo");
                    other.GetComponent<statsEnemigo4>().vulnerable = false;
                    other.GetComponent<statsEnemigo4>().recibirDano(player.GetComponent<statsJugador>().danoMelee, transform.position, player.GetComponent<statsJugador>().knockbackMelee, 0);
                }
            }

        }
        /*
        else if (other.CompareTag("boss") && player.GetComponentInChildren<Animator>().GetCurrentAnimatorClipInfo(1)[0].clip.name == "Clip1")
        {
            while (!other.transform.parent.CompareTag("jefe1"))
            {
                other = other.transform.parent.gameObject;
            }
            if (other.GetComponent<statsBoss1>().vulnerable == true)
            {
                //Debug.Log("Se hace daño al enemigo");
                other.GetComponent<statsBoss1>().vulnerable = false;
                other.GetComponent<statsBoss1>().recibirDano(player.GetComponent<statsJugador>().danoMelee, transform.position, 0);
            }
            else
            {
                //Debug.Log("no recibe daño");
            }
        }*/
    }
}
