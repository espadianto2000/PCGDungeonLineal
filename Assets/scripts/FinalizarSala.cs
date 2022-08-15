using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalizarSala : MonoBehaviour
{
    public bool finalizado = false;
    private void OnTriggerEnter(Collider other)
    {
        if (!finalizado)
        {
            finalizado = true;
            transform.parent.GetComponent<updateCam>().FinalizarSala();
        }
        
    }

}
