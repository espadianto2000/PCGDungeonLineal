using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class actualizarPosi : MonoBehaviour
{
    public GameObject aSeguir;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (aSeguir != null)
        {
            transform.position = aSeguir.transform.position;
        }
    }
}
