using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vulnerablesEnemigos : MonoBehaviour
{
    public GameObject trail;
    public AudioSource audioEspada;
    public AudioClip swing;
    // Start is called before the first frame update
    public void ActivarVulnerabilidad()
    {
        trail.SetActive(false);
        GameObject[] enemigos = GameObject.FindGameObjectsWithTag("enemigo");
        foreach (GameObject en in enemigos)
        {
            if(en.name.Contains("enemigoV1"))
            {
                en.GetComponent<statsEnemigo>().vulnerable = true;
            }
            else if(en.name.Contains("enemigoV2"))
            {
                en.GetComponent<statsEnemigo2>().vulnerable = true;
            }
            else if (en.name.Contains("enemigoV3"))
            {
                en.GetComponent<statsEnemigo3>().vulnerable = true;
            }
            else if (en.name.Contains("enemigoV4"))
            {
                en.GetComponent<statsEnemigo4>().vulnerable = true;
            }
        }
        if (GameObject.FindGameObjectWithTag("jefe1"))
        {
            GameObject.FindGameObjectWithTag("jefe1").GetComponent<statsBoss1>().vulnerable = true;
        }
        if (GameObject.FindGameObjectWithTag("jefe2"))
        {
            GameObject.FindGameObjectWithTag("jefe2").GetComponent<statsBoss2>().vulnerable = true;
        }
        if (GameObject.FindGameObjectWithTag("jefe3"))
        {
            GameObject.FindGameObjectWithTag("jefe3").GetComponent<statsBoss3>().vulnerable = true;
        }
    }
    public void slash()
    {
        audioEspada.clip = swing;
        audioEspada.Play();
    }
}
