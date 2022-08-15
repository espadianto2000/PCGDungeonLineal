using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController3 : MonoBehaviour
{
    public GameObject player;
    public UnityEngine.AI.NavMeshAgent agent;
    public statsEnemigo3 stats;
    public Vector3 posicionEstatica = new Vector3(0, 0, 0);
    public bool knock = false;
    private IEnumerator coroutine;
    Vector3 direction;
    public float knockSpeed = 3;
    public List<GameObject> enemigosASeguir = new List<GameObject>();
    public GameObject enemigoASeguir;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player");
        agent.speed = stats.velocidad;
        agent.acceleration = stats.velocidad * 2;
        foreach (Transform child in transform.parent)
        {
            if (child.CompareTag("enemigo") && child.gameObject.GetInstanceID() != gameObject.GetInstanceID())
            {
                enemigosASeguir.Add(child.gameObject);
            }
        }
        int ordenRandom = Random.Range(0, enemigosASeguir.Count);
        enemigoASeguir = enemigosASeguir[ordenRandom];
    }

    private void FixedUpdate()
    {
        if (!knock)
        {
            if(enemigoASeguir != null)
            {
                agent.SetDestination(enemigoASeguir.transform.position);
            }
            else if(enemigosASeguir.Count > 0)
            {
                //enemigosASeguir.Clear();
                for(int i = 0;i<enemigosASeguir.Count; i++)
                {
                    if (enemigosASeguir[i] == null)
                    {
                        enemigosASeguir.RemoveAt(i);
                    }
                }
                enemigoASeguir = enemigosASeguir.Count > 0 ? enemigosASeguir[Random.Range(0, enemigosASeguir.Count)] : null;
                if (enemigoASeguir != null) { agent.SetDestination(enemigoASeguir.transform.position); }
            }
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
            collision.transform.GetComponent<statsJugador>().recibirDano(GetComponent<statsEnemigo3>().danoMelee);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("player"))
        {
            collision.transform.GetComponent<statsJugador>().recibirDano(GetComponent<statsEnemigo3>().danoMelee);
        }
    }
}
