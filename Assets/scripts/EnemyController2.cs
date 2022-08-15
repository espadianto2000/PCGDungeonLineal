using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController2 : MonoBehaviour
{
    public GameObject player;
    public UnityEngine.AI.NavMeshAgent agent;
    public statsEnemigo2 stats;
    public Vector3 posicionEstatica = new Vector3(0, 0, 0);
    public bool knock = false;
    private IEnumerator coroutine;
    Vector3 direction;
    public float knockSpeed = 3;
    public float CooldownDisparo = 5;
    public GameObject spikeball;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player");
        agent.speed = stats.velocidad;
        agent.acceleration = stats.velocidad * 2;
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        if (CooldownDisparo > 0)
        {
            CooldownDisparo -= Time.deltaTime;
        }
        if (Vector3.Distance(player.transform.position, transform.position) >= 2.5f)
        {
            //agent.Resume();
            agent.SetDestination(player.transform.position);
        }
        else
        {
            if (!knock)
            {
                agent.ResetPath();
            }
            if (CooldownDisparo <= 0)
            {
                //Disparar
                ataqueDistancia();
                CooldownDisparo = stats.velocidadAtaque;
            }
        }

        if(knock)
        {
            knock = false;
            agent.velocity = direction * knockSpeed;
            agent.speed = stats.velocidad;
            agent.angularSpeed = 180;
            agent.acceleration = stats.velocidad * 2;
        }
    }

    void Update()
    {
        Quaternion targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);
        targetRotation.x = 0;
        targetRotation.z = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
    }

    private void ataqueDistancia()
    {
        GameObject obj = Instantiate(spikeball,transform.position,Quaternion.identity);
        obj.transform.localPosition = new Vector3(transform.position.x, 0.8f, transform.position.z);
        obj.GetComponent<proyectilEnemigo>().dano = stats.danoRango;
        obj.GetComponent<proyectilEnemigo>().velocidadProyectil = stats.velocidadProyectil;
        obj.GetComponent<proyectilEnemigo>().tiempoVida = stats.rango;
        obj.GetComponent<proyectilEnemigo>().destino = player.transform.position;
    }
    public void knocks(Vector3 dir, float knockbackMelee, int tipoAtaque)
    {
        StartCoroutine(knockback(dir, knockbackMelee, tipoAtaque));
    }
    IEnumerator knockback(Vector3 dir, float knockbackMelee, int tipoAtaque)
    {
        direction = dir.normalized;
        //Debug.Log("direccion normalizada: " + dir.normalized);
        //Vector3 velocidadG = transform.GetComponent<Rigidbody>().velocity;
        //Vector3 velocidadAngG = transform.GetComponent<Rigidbody>().angularVelocity;
        //transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
        //transform.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        if (tipoAtaque == 1)
        {
            knockSpeed = knockbackMelee * 0.25f;
            knock = true;
            /*agent.speed = stats.velocidad;
            agent.angularSpeed = 0;
            agent.acceleration = stats.velocidad * 3;*/
            /*yield return new WaitForSeconds(0.05f);
            knock = false;
            agent.speed = stats.velocidad;
            agent.angularSpeed = 180;
            agent.acceleration = stats.velocidad * 2;*/
            yield return null;
            //transform.GetComponent<Rigidbody>().AddForce(dir.normalized * knockbackMelee, ForceMode.Impulse);
        }
        else if (tipoAtaque == 0)
        {
            knockSpeed = knockbackMelee * 2.5f;
            knock = true;
            /*agent.speed = stats.velocidad;
            agent.angularSpeed = 0;
            agent.acceleration = stats.velocidad * 3;*/
            /*yield return new WaitForSeconds(0.05f);
            knock = false;
            agent.speed = stats.velocidad;
            agent.angularSpeed = 180;
            agent.acceleration = stats.velocidad * 2;*/
            yield return null;
            //transform.GetComponent<Rigidbody>().AddForce(dir.normalized * knockbackMelee, ForceMode.Impulse);
        }
        
    }
    IEnumerator activarKnock(Vector3 velocidadG, Vector3 velocidadAngG, float tiempo)
    {
        yield return new WaitForSeconds(tiempo);
        knock = false;
        //transform.GetComponent<Rigidbody>().velocity = velocidadG;
        //transform.GetComponent<Rigidbody>().angularVelocity = velocidadAngG;
        //transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
        //transform.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        yield return null;
    }
    private void OnCollisionStay(Collision collision)
    {
        //Debug.Log(collision.transform.name);
        if (collision.transform.CompareTag("player"))
        {
            collision.transform.GetComponent<statsJugador>().recibirDano(GetComponent<statsEnemigo2>().danoMelee);
        }
    }
    
}
