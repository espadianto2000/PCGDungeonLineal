using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossController3 : MonoBehaviour
{
    public GameObject player;
    public float timerAtaqueRango;
    public float timerSpawnEnemigos;
    public int estado = 0;
    public Animator anim;
    private Vector3 posCentral;
    private Vector3 puntoATraslado;
    public statsBoss3 stats;
    private bool ocupado = false;
    private bool espera = false;
    public GameObject spikeball;
    // Start is called before the first frame update
    void Start()
    {
        posCentral = new Vector3(transform.parent.position.x,transform.position.y, transform.parent.position.z);
        puntoATraslado = transform.position;
        timerAtaqueRango = stats.ataqueDistanciaTiempo;
        timerSpawnEnemigos = stats.spawnEnemigosTiempo;
    }
    // Update is called once per frame
    void Update()
    {
        if (player == null)
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
            if(!stats.muerto && timerSpawnEnemigos > 0 && timerAtaqueRango > 0)
            {
                timerAtaqueRango -= Time.deltaTime;
                timerSpawnEnemigos -= Time.deltaTime;
                if (Vector3.Distance(player.transform.position, transform.position) < 3.5f)
                {
                    puntoATraslado = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
                }
                if (Vector3.Distance(transform.position, puntoATraslado) < 0.5f && !espera)
                {
                    anim.SetTrigger("dejarCaminar");
                    espera = true;
                    Invoke("ResetDestino", 1.5f);
                }
                else if(Vector3.Distance(transform.position, puntoATraslado) >= 0.5f)
                {
                    if(anim.GetCurrentAnimatorClipInfo(0)[0].clip.name != "Walk")
                    {
                        anim.SetTrigger("caminar");
                    }
                    transform.position = Vector3.MoveTowards(transform.position, puntoATraslado, stats.velocidad * Time.deltaTime * 0.3f);
                    Quaternion targetRotation = Quaternion.LookRotation(puntoATraslado - transform.position);
                    targetRotation.x = 0;
                    targetRotation.z = 0;
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 150f * Time.deltaTime);                    
                }
            }
            else if(!stats.muerto && timerSpawnEnemigos < 0 && !ocupado)
            {
                ocupado = true;
                anim.SetTrigger("atacar2");
            }
            else if(!stats.muerto && timerAtaqueRango < 0 && !ocupado)
            {
                ocupado = true;
                anim.SetTrigger("atacar1");
            }
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (GetComponent<statsBoss3>().vida > 0)
        {
            if (other.CompareTag("proyectilJugador"))
            {
                GetComponent<statsBoss3>().recibirDano(player.GetComponent<statsJugador>().danoRango, other.transform.position, 1);
                Destroy(other.gameObject);
            }
            if (other.transform.CompareTag("obstaculo"))
            {
                //GameObject.Find("NavMesh").GetComponent<UnityEngine.AI.NavMeshSurface>().BuildNavMesh();
                Destroy(other.gameObject);
            }
            if (other.transform.CompareTag("sword") && player.GetComponentInChildren<Animator>().GetCurrentAnimatorClipInfo(1)[0].clip.name == "Clip1")
            {
                if (GetComponent<statsBoss3>().vulnerable)
                {
                    GetComponent<statsBoss3>().vulnerable = false;
                    GetComponent<statsBoss3>().recibirDano(player.GetComponent<statsJugador>().danoMelee, other.GetComponent<atacando>().player.transform.position, 0);
                }
            }
            if (other.CompareTag("player") && GetComponent<statsBoss3>().vida > 0)
            {
                other.GetComponent<statsJugador>().recibirDano(GetComponent<statsBoss3>().danoMelee);
            }
        }
    }
    private void ResetDestino()
    {   
        float xPos = posCentral.x + Random.Range(-4f, 4f);
        float zPos = posCentral.z + Random.Range(-4f, 4f);
        puntoATraslado = new Vector3(xPos, transform.position.y, zPos);
        espera = false;
    }
    private void atacarADistancia()
    {
        int proyectiles = Random.Range(stats.maxProyectiles - 2, stats.maxProyectiles);
        Debug.Log("spawn proyectiles: " + proyectiles);
        for(int i = 0; i<proyectiles; i++)
        {
            GameObject obj = Instantiate(spikeball, transform.position, Quaternion.identity);
            obj.transform.localPosition = new Vector3(transform.position.x, 0.8f, transform.position.z);
            obj.GetComponent<proyectilEnemigo>().dano = stats.danoRango;
            obj.GetComponent<proyectilEnemigo>().velocidadProyectil = stats.velocidadProyectil;
            obj.GetComponent<proyectilEnemigo>().tiempoVida = stats.rango;
            float xPos = posCentral.x + Random.Range(-4f, 4f);
            float zPos = posCentral.z + Random.Range(-4f, 4f);
            obj.GetComponent<proyectilEnemigo>().destino = new Vector3(xPos,obj.transform.position.y,zPos);
        }
        
    }
    private void SpawnearEnemigos()
    {
        int enemigos = Random.Range(1, stats.maxEnemigosSpawneados);
        Debug.Log("spawn enemigos: " + enemigos);
        for(int i = 0; i < enemigos; i++)
        {
            float xPos = transform.position.x + Random.Range(-1f, 1f);
            float zPos = transform.position.z + Random.Range(-1f, 1f);
            Vector3 pos = new Vector3(xPos, transform.position.y, zPos);
            GameObject obj = Instantiate(transform.parent.GetComponentInChildren<generarDistribucion>().enemigos[Random.Range(0, 4)], pos, Quaternion.identity);
            obj.transform.parent = transform.parent.GetComponentInChildren<updateCam>().transform;
            transform.parent.GetComponentInChildren<updateCam>().contadorEnemigos++;
            transform.parent.GetComponentInChildren<updateCam>().enemigosInstanciados.Add(obj);
            obj.SetActive(true);
        }
        
    }
    public void ResetTimerSpawnEnemigos()
    {
        timerSpawnEnemigos = stats.spawnEnemigosTiempo;
        ocupado = false;
    }
    public void ResetTimerAtaqueDistancia()
    {
        timerAtaqueRango = stats.ataqueDistanciaTiempo;
        ocupado = false;
    }
}
