using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnMuros : MonoBehaviour
{
    private salas salas;
    // Start is called before the first frame update
    void Start()
    {
        salas = GameObject.FindGameObjectWithTag("salas").GetComponent<salas>();
        spawnMuro();
    }

    // Update is called once per frame
    void spawnMuro()
    {
        Instantiate(salas.muros, transform.position, Quaternion.identity);
    }
}
