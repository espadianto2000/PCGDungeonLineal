using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class salas : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> salasIzq;
    public List<GameObject> salasArr;
    public List<GameObject> salasDer;
    public List<GameObject> salasAba;
    public int contadorSalas;
    public int Limite1; 
    public int Limite2;
    public bool lim1=false;
    public bool lim2=false;
    public float timer = 30000;
    public bool spawnedBoss = false;
    public GameObject jefeinstanciado;

    public dificultadLineal dl;
    public List<GameObject> salasInstanciadas;
    public GameObject[] boss;
    public GameObject SpawnMuros;
    public GameObject muros;
    public bool juegoListo = false;
    public GameObject player;
    public GameObject SalaLimite;
    public UnityEngine.AI.NavMeshSurface surface;
    public int salasSuperadas = 0;
    public gameManager gm;
    public float t1=0;
    bool term = false;

    public List<Vector3> posiciones;
    private void Start()
    {
        dl = GameObject.Find("dificultad").GetComponent<dificultadLineal>();
        Limite1 = dl.numSalas;
        Limite2 = dl.numSalas * 2;
        gm = GameObject.Find("GameManager").GetComponent<gameManager>();
        player = gm.player;
        gm.ajustarMouse();
        t1 = Time.realtimeSinceStartup;
        surface = GameObject.Find("NavMesh").GetComponent<UnityEngine.AI.NavMeshSurface>();
    }
    private void LateUpdate()
    {
        term = true;
        foreach (GameObject sp in GameObject.FindGameObjectsWithTag("SpawnPoint"))
        {
            if (!sp.GetComponent<generarSala>().spawned)
            {
                if (contadorSalas >= Limite2)
                {
                    sp.GetComponent<generarSala>().SpawnMuro();
                    sp.GetComponent<generarSala>().spawned = true;
                }
                term = false;
            }
        }
    }

    private void Update()
    {
        if (!spawnedBoss)
        {
            if (contadorSalas >= Limite1 && lim1 == false)
            {
                salasIzq.RemoveRange(salasIzq.Count - 5, 5);
                salasDer.RemoveRange(salasDer.Count - 5, 5);
                salasArr.RemoveRange(salasArr.Count - 5, 5);
                salasAba.RemoveRange(salasAba.Count - 5, 5);
                for (int i = 0; i < 4; i++)
                {
                    salasIzq.Add(salasIzq[0]);
                    salasDer.Add(salasDer[0]);
                    salasArr.Add(salasArr[0]);
                    salasAba.Add(salasAba[0]);
                }
                lim1 = true;
            }
            if (contadorSalas >= Limite2 && lim2 == false)
            {
                salasIzq.RemoveRange(1, 8);
                salasDer.RemoveRange(1, 8);
                salasArr.RemoveRange(1, 8);
                salasAba.RemoveRange(1, 8);
                lim2 = true;
            }
            if (timer <= 0 && !spawnedBoss && salasInstanciadas.Count >= 1 && term)
            {
                int orden = Random.Range(0, boss.Length);
                refrescarNavMesh();
                GameObject jefe = Instantiate(boss[orden], salasInstanciadas[salasInstanciadas.Count - 1].transform.position, Quaternion.identity);
                jefe.transform.parent = salasInstanciadas[salasInstanciadas.Count - 1].transform;
                switch (orden)
                {
                    case 0:
                        stBoss1 stJ11 = new stBoss1(dl.nivelDificultad);
                        stBoss1 stJ12 = new stBoss1(dl.nivelDificultad+1);
                        jefe.GetComponent<statsBoss1>().vidaMax = Random.Range(stJ11.vidaMax, stJ12.vidaMax);
                        jefe.GetComponent<statsBoss1>().velocidad = Random.Range(stJ11.velocidad, stJ12.velocidad);
                        jefe.GetComponent<statsBoss1>().danoMelee = stJ11.danoMelee;
                        jefe.GetComponent<statsBoss1>().danoRango = stJ11.danoRango;
                        jefe.GetComponent<statsBoss1>().timerAtaq = Random.Range(stJ11.timerAtaq, stJ12.timerAtaq);
                        jefe.GetComponent<statsBoss1>().velocidadGiro = Random.Range(stJ11.velocidadGiro, stJ12.velocidadGiro);
                        break;
                    case 1:
                        stBoss2 stJ21 = new stBoss2(dl.nivelDificultad);
                        stBoss2 stJ22 = new stBoss2(dl.nivelDificultad + 1);
                        jefe.GetComponent<statsBoss2>().vidaMax = Random.Range(stJ21.vidaMax, stJ22.vidaMax);
                        jefe.GetComponent<statsBoss2>().velocidad = Random.Range(stJ21.velocidad, stJ22.velocidad);
                        jefe.GetComponent<statsBoss2>().danoMelee = stJ21.danoMelee;
                        jefe.GetComponent<statsBoss2>().velocidadExtra = Random.Range(stJ21.velocidadExtra, stJ22.velocidadExtra);
                        jefe.GetComponent<statsBoss2>().rebotesMax = Random.Range(stJ21.rebotesMax, stJ22.rebotesMax);
                        break;
                    case 2:
                        stBoss3 stJ31 = new stBoss3(dl.nivelDificultad);
                        stBoss3 stJ32 = new stBoss3(dl.nivelDificultad + 1);
                        jefe.GetComponent<statsBoss3>().vidaMax = Random.Range(stJ31.vidaMax, stJ32.vidaMax);
                        jefe.GetComponent<statsBoss3>().velocidad = Random.Range(stJ31.velocidad, stJ32.velocidad);
                        jefe.GetComponent<statsBoss3>().danoMelee = stJ31.danoMelee;
                        jefe.GetComponent<statsBoss3>().danoRango = stJ31.danoRango;
                        jefe.GetComponent<statsBoss3>().rango = Random.Range(stJ31.rango, stJ32.rango);
                        jefe.GetComponent<statsBoss3>().ataqueDistanciaTiempo = Random.Range(stJ31.ataqueDistanciaTiempo, stJ32.ataqueDistanciaTiempo);
                        jefe.GetComponent<statsBoss3>().spawnEnemigosTiempo = Random.Range(stJ31.spawnEnemigosTiempo, stJ32.spawnEnemigosTiempo);
                        jefe.GetComponent<statsBoss3>().maxEnemigosSpawneados = stJ31.maxEnemigosSpawneados;
                        jefe.GetComponent<statsBoss3>().maxProyectiles = stJ31.maxProyectiles;
                        jefe.GetComponent<statsBoss3>().velocidadProyectil = Random.Range(stJ31.velocidadProyectil, stJ32.velocidadProyectil);
                        break;
                }
                spawnedBoss = true;
                jefeinstanciado = jefe;
            }
            else
            {
                timer = timer - Time.deltaTime;
            }
        }else if (!juegoListo)
        {
            Invoke("desvanecerElementos", 2f);
            borrarSpawners();
            juegoListo = true;
            gm.tiempoNivel = 0;
            float t2 = Time.realtimeSinceStartup;
            Debug.Log("tiempo de generacíón de salas: " + (t2 - t1));
            
        }
        
    }
    private void desvanecerElementos()
    {
        gm.panelCarga.GetComponent<panelCarga>().desvanecerElementos();
    }

    void borrarSpawners()
    {
        GameObject[] spawners = GameObject.FindGameObjectsWithTag("SpawnPoint");
        foreach(GameObject sp in spawners)
        {
            Vector3 temp = sp.transform.position;
            GameObject padre = sp.GetComponent<generarSala>().salaGenerada;
            
            Destroy(sp.gameObject);
            if (padre != null)
            {

                var lim = Instantiate(SalaLimite, temp, Quaternion.identity);
                lim.transform.SetParent(padre.transform);
                if (padre == salasInstanciadas[salasInstanciadas.Count - 1])
                {
                    lim.GetComponent<updateCam>().boss = jefeinstanciado;
                }
            }
        }
        GameObject[] spawnmuros = GameObject.FindGameObjectsWithTag("spawnMuro");
        foreach (GameObject sp in spawnmuros)
        {
            Destroy(sp.gameObject);
        }
            player.SetActive(true);
        player.GetComponent<statsJugador>().iniciar();
    }
    void refrescarNavMesh()
    {
        surface.BuildNavMesh();
    }

}

