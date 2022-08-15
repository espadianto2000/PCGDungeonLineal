using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using Unity.Services.Core;
//using Unity.Services.Analytics;

public class portalSiguienteNivel : MonoBehaviour
{
    public gameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<gameManager>();

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
            //Debug.Log("Analytics : " + gm.identificadorMaq + "--" + "nivelFinalizado");
            /*AnalyticsService.Instance.CustomData("nivelFinalizado", new Dictionary<string, object>
                {
                    { "UserRun",gm.identificadorMaq},
                    { "nivelActual", GameObject.Find("dificultad").GetComponent<dificultadLineal>().nivelDificultad },
                    { "tiempo", gm.tiempoNivel },
                    { "danoRecibido", other.GetComponent<charController>().danoNivel },
                    { "PremiosNivel", gm.numeroPremiosNivel},
                    { "salasNivel", GameObject.Find("salas(Clone)").GetComponent<salas>().contadorSalas+1},
                    { "salasCompletadas", GameObject.Find("salas(Clone)").GetComponent<salas>().salasSuperadas}
                });
            try
            {
                AnalyticsService.Instance.Flush();
            }catch
            {
            }*/
            Debug.Log("nivelFinalizado: "+Analytics.IsCustomEventEnabled("nivelFinalizado"));
            AnalyticsResult anRes = Analytics.CustomEvent("nivelFinalizado-"+ gm.identificadorMaq+"-"+ GameObject.Find("dificultad").GetComponent<dificultadLineal>().nivelDificultad, new Dictionary<string, object>
                {
                    /*{ "UserRun",gm.identificadorMaq},
                    { "nivelActual", GameObject.Find("dificultad").GetComponent<dificultadLineal>().nivelDificultad },*/
                    { "tiempo", gm.tiempoNivel },
                    { "danoRecibido", other.GetComponent<charController>().danoNivel },
                    { "PremiosNivel", gm.numeroPremiosNivel},
                    { "salasNivel", GameObject.Find("salas(Clone)").GetComponent<salas>().contadorSalas+1},
                    { "salasCompletadas", GameObject.Find("salas(Clone)").GetComponent<salas>().salasSuperadas}
                });
            Debug.Log("analyticsResult nivelFinalizado: " + anRes);
            Analytics.FlushEvents();
            //gm.analyticsNextLevel();
            other.GetComponent<charController>().danoNivel = 0;
            gm.numeroPremiosNivel = 0;
            gm.NextLevel();
        }
    }
}
