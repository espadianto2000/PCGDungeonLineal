using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generarSala : MonoBehaviour
{
    public int Direccion;
    public bool destroyer = false;
    public gameManager gm;

    private salas salas;
    private int rand;
    public bool spawned = false;
    public GameObject salaGenerada=null;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<gameManager>();
        salas = gm.salasActuales;
        if (!salas.posiciones.Contains(transform.position))
        {
            salas.posiciones.Add(transform.position);
            Spawn();
        }
        else Destroy(gameObject);
        //Invoke("Spawn", 0.1F);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (destroyer)
        { 
            if(!other.CompareTag("boss"))
            other.gameObject.SetActive(false);
            //Debug.Log("desactivando: " + other.name + "; de: " + other.transform.parent.name);
            //Destroy(other.gameObject);
        }
        else
        {
            if (other.CompareTag("SpawnPoint"))
            {
                if (other.GetComponent<generarSala>().spawned == true)
                {
                    //Debug.Log("destruyendo: " + gameObject.name + "; de: " + gameObject.transform.parent.name);
                    Destroy(gameObject);
                }
            }
        }
        
    }
    void Spawn()
    {
        if (!spawned && salas.contadorSalas < salas.Limite2)
        {
            GameObject inst = null;
            switch (Direccion)
            {
                case 1:
                    rand = Random.Range(0, salas.salasDer.Count);
                    inst = Instantiate(salas.salasDer[rand], transform.position, new Quaternion(0, 0, 0, 0));
                    //salas.salasInstanciadas.Add(inst);
                    salaGenerada = inst.gameObject;
                    //Debug.Log("Instanciado: " + inst.name + "; desde: " + transform.parent.name);
                    break;
                case 2:
                    rand = Random.Range(0, salas.salasAba.Count);
                    inst = Instantiate(salas.salasAba[rand], transform.position, new Quaternion(0, 0, 0, 0));
                    //salas.salasInstanciadas.Add(inst);
                    salaGenerada = inst.gameObject;
                    //Debug.Log("Instanciado: " + inst.name + "; desde: " + transform.parent.name);
                    break;
                case 3:
                    rand = Random.Range(0, salas.salasIzq.Count);
                    inst = Instantiate(salas.salasIzq[rand], transform.position, new Quaternion(0, 0, 0, 0));
                    //salas.salasInstanciadas.Add(inst);
                    salaGenerada = inst.gameObject;
                    //Debug.Log("Instanciado: " + inst.name + "; desde: " + transform.parent.name);
                    break;
                case 4:
                    rand = Random.Range(0, salas.salasArr.Count);
                    inst = Instantiate(salas.salasArr[rand], transform.position, new Quaternion(0, 0, 0, 0));
                    //salas.salasInstanciadas.Add(inst);
                    salaGenerada = inst.gameObject;
                    //Debug.Log("Instanciado: " + inst.name + "; desde: " + transform.parent.name);
                    break;
            }
            destroyer = true;
            salas.salasInstanciadas.Add(inst);
            salas.contadorSalas++;
            salas.timer = 0.01f;
            spawned = true;
        }
    }
    public void SpawnMuro()
    {
        Instantiate(salas.SpawnMuros, transform.position, Quaternion.identity);
    }
    
}
