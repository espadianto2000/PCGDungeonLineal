using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using Unity.Services.Core;
using Newtonsoft.Json;
using UnityEngine.Networking;
using System.Text;
//using Unity.Services.Analytics;

public class NivelStatic
{
    public string usuario;
    public string inicioRun;
    public int nivel;
    public int premiosNivel;
    public float tiempo;
    public int salasTotales;
    public int salasCompletadas;
    public int danoRecibido;
    public NivelStatic(string us, string ini, int niv, int prem, float t, int st, int sc, int dano)
    {
        this.usuario = us;
        this.inicioRun = ini;
        this.nivel = niv;
        this.premiosNivel = prem;
        this.tiempo = t;
        this.salasTotales = st;
        this.salasCompletadas = sc;
        this.danoRecibido = dano;
    }
}

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

            //---------------PruebaFirebaseNest--------------
            string id = gm.identificadorMaq.Replace('.', ',');
            id = id.Replace('/', ',');
            string[] ident = id.Split('|');
            GameObject dl = GameObject.Find("dificultad");
            NivelStatic nv = new NivelStatic(ident[0], ident[1], dl.GetComponent<dificultadLineal>().nivelDificultad, gm.numeroPremiosNivel, gm.tiempoNivel, GameObject.Find("salas(Clone)").GetComponent<salas>().contadorSalas + 1, GameObject.Find("salas(Clone)").GetComponent<salas>().salasSuperadas, other.GetComponent<charController>().danoNivel);
            string jsonString = JsonConvert.SerializeObject(nv);
            StartCoroutine(nivelCompletado(jsonString));
            //-----------------------------------------------
            /*Debug.Log("nivelFinalizado: "+Analytics.IsCustomEventEnabled("nivelFinalizado"));
            AnalyticsResult anRes = Analytics.CustomEvent("nivelFinalizado-"+ gm.identificadorMaq+"-"+ GameObject.Find("dificultad").GetComponent<dificultadLineal>().nivelDificultad, new Dictionary<string, object>
                {
                    { "tiempo", gm.tiempoNivel },
                    { "danoRecibido", other.GetComponent<charController>().danoNivel },
                    { "PremiosNivel", gm.numeroPremiosNivel},
                    { "salasNivel", GameObject.Find("salas(Clone)").GetComponent<salas>().contadorSalas+1},
                    { "salasCompletadas", GameObject.Find("salas(Clone)").GetComponent<salas>().salasSuperadas}
                });
            Debug.Log("analyticsResult nivelFinalizado: " + anRes);
            Analytics.FlushEvents();*/
            other.GetComponent<charController>().danoNivel = 0;
            gm.numeroPremiosNivel = 0;
            gm.NextLevel();
        }
    }
    IEnumerator nivelCompletado(string js)
    {
        UnityWebRequest uwr = new UnityWebRequest("https://pcg-nest.herokuapp.com/nivelStatic", "POST");
        byte[] xmlToSend = Encoding.UTF8.GetBytes(js);
        uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(xmlToSend);
        string cadenadeXML = Encoding.UTF8.GetString(xmlToSend);
        Debug.Log(cadenadeXML);
        uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");
        yield return uwr.SendWebRequest();
        if (uwr.isNetworkError || uwr.isHttpError)
        {
            string servicioResult2 = uwr.downloadHandler.text;
            Debug.Log("error webrequest: " + servicioResult2);
            Debug.Log("statusCode: " + uwr.responseCode);
            uwr.Dispose();
            yield break;
        }
        else
        {
            Debug.Log("se envio la data");
            Debug.Log("statusCode: " + uwr.responseCode);
            uwr.Dispose();
            yield break;
        }
    }
}
