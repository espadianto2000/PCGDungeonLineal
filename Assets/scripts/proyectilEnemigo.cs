using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class proyectilEnemigo : MonoBehaviour
{
    public int dano;
    public float velocidadProyectil;

    public Vector3 destino;
    public Vector3 pos;
    public bool started = false;
    public Vector3 direccion;
    public Rigidbody rb;
    public float timerVida = 0;
    public float tiempoVida;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player");
        Invoke("delete", 5f);
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
        timerVida += Time.deltaTime;
        direccion = destino - pos;
        transform.Translate(direccion.normalized * velocidadProyectil * Time.deltaTime);
        if (timerVida > tiempoVida)
        {
            rb.constraints = RigidbodyConstraints.None;
        }
    }
    private void delete()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
            other.GetComponent<statsJugador>().recibirDano(dano);
            Destroy(gameObject);
        }
    }
}
