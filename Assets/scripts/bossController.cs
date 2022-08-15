using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossController : MonoBehaviour
{
    public GameObject player;
    public float timer=5;
    public int estado=0;
    public Animator anim;
    private bool atk = false;
    private bool caminando = false;
    public ParticleSystem flama;
    public GameObject cabeza;
    // Start is called before the first frame update
    void Start()
    {
        flama.enableEmission = false;
        flama.GetComponent<datosFuego>().dano = GetComponent<statsBoss1>().danoRango;
        timer = GetComponent<statsBoss1>().timerAtaq;
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null)
        {
            try
            {
                player = GameObject.Find("player");
            }
            catch
            {

            }
        }
        if (player != null)
        {
            if(estado == 0)
            {
                Quaternion targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);
                targetRotation.x = 0;
                targetRotation.z = 0;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, GetComponent<statsBoss1>().velocidadGiro * Time.deltaTime);
                if (!caminando)
                {
                    anim.SetTrigger("caminar");
                    caminando = true;
                }
                //Debug.Log("diferencia entre quaternions: " + (transform.rotation.normalized * targetRotation.normalized));
                if(Vector3.Distance(player.transform.position, transform.position) > 2f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z), GetComponent<statsBoss1>().velocidad * Time.deltaTime);
                }
            }
            else if(estado == 1 && !atk)
            {
                //ataque fuego
                anim.SetTrigger("atk1");
                atk = true;
            }
            else if(estado == 2 && !atk)
            {
                //ataque fisico
                anim.SetTrigger("atk2");
                atk = true;
            }
            if(timer <= 0)
            {
                caminando = false;
                if(Vector3.Distance(player.transform.position, transform.position) > 3f)
                {
                    estado = 1;
                }
                else
                {
                    estado = 2;
                }
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }
    }
    public void cambiarEstado()
    {
        estado = 0;
        timer = GetComponent<statsBoss1>().timerAtaq;
        atk = false;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (GetComponent<statsBoss1>().vida > 0)
        {
            if (other.CompareTag("proyectilJugador"))
            {
                GetComponent<statsBoss1>().recibirDano(player.GetComponent<statsJugador>().danoRango, other.transform.position, 1);
                Destroy(other.gameObject);
            }
            if (other.transform.CompareTag("obstaculo"))
            {
                Destroy(other.gameObject);
            }
            if (other.transform.CompareTag("sword") && player.GetComponentInChildren<Animator>().GetCurrentAnimatorClipInfo(1)[0].clip.name == "Clip1")
            {
                if (GetComponent<statsBoss1>().vulnerable)
                {
                    GetComponent<statsBoss1>().vulnerable = false;
                    GetComponent<statsBoss1>().recibirDano(player.GetComponent<statsJugador>().danoMelee, other.GetComponent<atacando>().player.transform.position, 0);
                }
            }
            if (other.CompareTag("player") && GetComponent<statsBoss1>().vida > 0)
            {
                other.GetComponent<statsJugador>().recibirDano(GetComponent<statsBoss1>().danoMelee);
            }
        }
    }
    public void activarFuego()
    {
        flama.enableEmission = true;

        //flama.transform.GetChild(0).GetComponent<ParticleSystem>().enableEmission = true;
    }
    public void desactivarFuego()
    {
        flama.enableEmission = false;
        //flama.transform.GetChild(0).GetComponent<ParticleSystem>().enableEmission = false;
    }
}
