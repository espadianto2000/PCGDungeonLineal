using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class proyectil : MonoBehaviour
{
    public float dano;

    public Vector3 destino;
    public Vector3 pos;
    public bool started = false;
    Vector3 direccion;
    public Rigidbody rb;
    public float timerVida=0;
    public float tiempoVida;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player");
        Invoke("delete", 7f);

    }

    // Update is called once per frame
    void Update()
    {
        if (!started)
        {
            pos = transform.position;
            pos.y = 0;
            destino.y = 0;
            started = true;
        }
        //Debug.Log("pos inicial: " + pos);
        //Debug.Log("pos destino: " + destino);
        timerVida += Time.deltaTime;
        direccion = destino - pos;
        //Debug.Log(direccion.normalized);
        transform.GetChild(0).transform.rotation = Quaternion.LookRotation(direccion.normalized);
        //-135,90,-90

        //Debug.Log("direccion: " + direccion.normalized);
        transform.Translate(direccion.normalized * 7f * Time.deltaTime);


        //rb.AddForce(direccion.normalized*10f*Time.deltaTime,ForceMode.VelocityChange); 
        if(timerVida > tiempoVida)
        {
            rb.constraints = RigidbodyConstraints.None;
            //Destroy(transform.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemigo"))
        {
            if (other.name.Contains("enemigoV1"))
            {
                other.GetComponent<statsEnemigo>().recibirDano(player.GetComponent<statsJugador>().danoRango, transform.position, player.GetComponent<statsJugador>().knockbackMelee, 1);
                Destroy(transform.gameObject);
            }
            else if (other.name.Contains("enemigoV2"))
            {
                other.GetComponent<statsEnemigo2>().recibirDano(player.GetComponent<statsJugador>().danoRango, transform.position, player.GetComponent<statsJugador>().knockbackMelee, 1);
                Destroy(transform.gameObject);
            }
            else if (other.name.Contains("enemigoV3"))
            {
                other.GetComponent<statsEnemigo3>().recibirDano(player.GetComponent<statsJugador>().danoRango, transform.position, player.GetComponent<statsJugador>().knockbackMelee, 1);
                Destroy(transform.gameObject);
            }
            else if (other.name.Contains("enemigoV4"))
            {
                other.GetComponent<statsEnemigo4>().recibirDano(player.GetComponent<statsJugador>().danoRango, transform.position, player.GetComponent<statsJugador>().knockbackMelee, 1);
                Destroy(transform.gameObject);
            }

        }
        else if (other.CompareTag("piso"))
        {
            Destroy(transform.gameObject);
        }
        
    }
    private void delete()
    {
        Destroy(gameObject);
    }
}
