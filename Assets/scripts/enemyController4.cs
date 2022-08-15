using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController4 : MonoBehaviour
{
    public GameObject player;
    public UnityEngine.AI.NavMeshAgent agent;
    public statsEnemigo4 stats;
    public Vector3 posicionEstatica = new Vector3(0, 0, 0);
    public bool knock = false;
    public Vector3 destino;
    Vector3 direction;
    public float knockSpeed = 3;
    Vector3 posCentral;
    public float timerCooldown;
    public GameObject explo;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player");
        timerCooldown = stats.cooldownAtaque;
        agent.speed = stats.velocidad;
        agent.acceleration = stats.velocidad * 2;
        posCentral = transform.parent.transform.position;
        float destX = 0;
        float destZ = 0;
        switch (Random.Range(0, 4))
        {
            case 0:
                destX = posCentral.x + (Random.Range(3f, 4.5f) * -1);
                destZ = posCentral.z + (Random.Range(3f, 4.5f));
                break;
            case 1:
                destZ = posCentral.z + (Random.Range(3f, 4.5f) * -1);
                destX = posCentral.x + (Random.Range(3f, 4.5f));
                break;
            case 2:
                destZ = posCentral.z + (Random.Range(3f, 4.5f) * -1);
                destX = posCentral.x + (Random.Range(3f, 4.5f) * -1);
                break;
            case 3:
                destX = posCentral.x + (Random.Range(3f, 4.5f));
                destZ = posCentral.z + (Random.Range(3f, 4.5f));
                break;
        }
        destino = new Vector3(destX, transform.position.y, destZ);
        //Debug.Log("destino: " + destino);
    }
    private void FixedUpdate()
    {
        if (!(knock) && Vector3.Distance(destino, transform.position) > 2)
        {
            agent.SetDestination(destino);
        }
        else if(knock)
        {
            knock = false;
            agent.velocity = direction * knockSpeed;
            agent.speed = stats.velocidad;
            agent.angularSpeed = 180;
            agent.acceleration = stats.velocidad * 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timerCooldown < 0)
        {
            //atacar
            timerCooldown = stats.cooldownAtaque;
            GameObject explosion = Instantiate(explo, Vector3.zero, Quaternion.identity);
            explosion.GetComponent<auraExplosion>().dano = stats.danoMelee;
        }
        else
        {
            timerCooldown -= Time.deltaTime;
        }
        Quaternion targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);
        targetRotation.x = 0;
        targetRotation.z = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
        if(Vector3.Distance(destino, transform.position) < 2)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < 3)
            {
                if(Mathf.Abs(player.transform.position.x-transform.position.x) > Mathf.Abs(player.transform.position.z - transform.position.z)){
                    destino.z = posCentral.z + ((destino.z - posCentral.z) * -1);
                    //Debug.Log("destino cuando mas cerca a x: " + destino);
                }
                else
                {
                    destino.x = posCentral.x + ((destino.x - posCentral.x) * -1);
                    //Debug.Log("destino cuando mas cerca a z: " + destino);
                }
            }
        }
    }
    public void knocks(Vector3 dir, float knockbackMelee, int tipoAtaque)
    {
        StartCoroutine(knockback(dir, knockbackMelee, tipoAtaque));
    }
    IEnumerator knockback(Vector3 dir, float knockbackMelee, int tipoAtaque)
    {
        direction = dir.normalized;
        if (tipoAtaque == 1)
        {
            knockSpeed = knockbackMelee * 0.25f;
            knock = true;
            yield return null;
        }
        else if (tipoAtaque == 0)
        {
            knockSpeed = knockbackMelee * 2.5f;
            knock = true;
            yield return null;
        }

    }
    IEnumerator activarKnock(Vector3 velocidadG, Vector3 velocidadAngG, float tiempo)
    {
        yield return new WaitForSeconds(tiempo);
        knock = false;
        yield return null;
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.CompareTag("player"))
        {
            collision.transform.GetComponent<statsJugador>().recibirDano(GetComponent<statsEnemigo4>().danoMelee);
        }
    }
    
}
