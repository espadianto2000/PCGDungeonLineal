using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlAura : MonoBehaviour
{
    bool crecer = false;
    float velocidadCrecimiento;
    bool curar = false;
    bool decrecer = false;
    public statsEnemigo3 stats;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("lanzarCuracion", stats.cooldownCuracion);
        velocidadCrecimiento = stats.velocidadCrecimiento;
    }

    // Update is called once per frame
    void Update()
    {
        if (curar)
        {
            transform.GetChild(0).transform.rotation = Quaternion.Euler(transform.GetChild(0).transform.rotation.eulerAngles + new Vector3(0, 0, Time.deltaTime * 75));
            transform.GetChild(1).transform.rotation = Quaternion.Euler(transform.GetChild(1).transform.rotation.eulerAngles - new Vector3(0, 0, Time.deltaTime * 75));
            if (transform.GetChild(0).transform.localScale.x<=4 && crecer)
            {
                transform.GetChild(0).transform.localScale += new Vector3(Time.deltaTime * velocidadCrecimiento, Time.deltaTime * velocidadCrecimiento, Time.deltaTime * velocidadCrecimiento);
                transform.GetChild(1).transform.localScale += new Vector3(Time.deltaTime * velocidadCrecimiento, Time.deltaTime * velocidadCrecimiento, Time.deltaTime * velocidadCrecimiento);
            }
            else
            {
                if (crecer)
                {
                    decrecer = true;
                    crecer = false;
                }
                if(transform.GetChild(0).transform.localScale.x >= 0 && decrecer)
                {
                    transform.GetChild(0).transform.localScale -= new Vector3(Time.deltaTime * velocidadCrecimiento, Time.deltaTime * velocidadCrecimiento, Time.deltaTime * velocidadCrecimiento);
                    transform.GetChild(1).transform.localScale -= new Vector3(Time.deltaTime * velocidadCrecimiento, Time.deltaTime * velocidadCrecimiento, Time.deltaTime * velocidadCrecimiento);
                }
                else
                {
                    decrecer = false;
                    transform.GetChild(0).transform.localScale = Vector3.zero;
                    transform.GetChild(1).transform.localScale = Vector3.zero;
                    curar = false;
                    Invoke("lanzarCuracion", stats.cooldownCuracion);
                }
            }
        }
    }
    void lanzarCuracion()
    {
        curar = true;
        crecer = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "enemigoV1(Clone)")
        {
            other.GetComponent<statsEnemigo>().curarDano(stats.curacion);
        }
        else if(other.name == "enemigoV2(Clone)")
        {
            other.GetComponent<statsEnemigo2>().curarDano(stats.curacion);
        }
        else if (other.name == "enemigoV3(Clone)")
        {
            other.GetComponent<statsEnemigo3>().curarDano(stats.curacion);
        }
        else if (other.name == "enemigoV4(Clone)")
        {
            other.GetComponent<statsEnemigo4>().curarDano(stats.curacion);
        }
    }
}
