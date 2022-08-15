using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    public GameObject player;
    public UnityEngine.AI.NavMeshAgent agent;
    public statsEnemigo stats;
    public Vector3 posicionEstatica=new Vector3(0,0,0);
    public bool knock = false;
    private IEnumerator coroutine;
    Vector3 direction;
    public float knockSpeed = 3;
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
        if (!(/*Vector3.Distance(player.transform.position, transform.position) < 0.5f ||*/ knock))
        {
            //agent.updatePosition = true;
            agent.SetDestination(player.transform.position);
        }
        else
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
    public void knocks(Vector3 dir, float knockbackMelee, int tipoAtaque)
    {
        StartCoroutine(knockback(dir,knockbackMelee,tipoAtaque));
    }
    IEnumerator knockback(Vector3 dir, float knockbackMelee, int tipoAtaque)
    {
        direction = dir.normalized;
        if(tipoAtaque == 1)
        {
            knockSpeed = knockbackMelee *0.25f;
            knock = true;
            yield return null;  
        }else if(tipoAtaque == 0)
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
            collision.transform.GetComponent<statsJugador>().recibirDano(this.GetComponent<statsEnemigo>().danoMelee);
        }
    }
}
