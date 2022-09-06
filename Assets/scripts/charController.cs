using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using System;
using Unity.Services.Core;
using Newtonsoft.Json;
using UnityEngine.Networking;
using System.Text;
//using Unity.Services.Analytics;

public class muerteStatic
{
    public string usuario;
    public string inicioRun;
    public int nivel;
    public float tiempo;
    public int salasTotales;
    public int salasCompletadas;
    public int danoRecibido;
    public muerteStatic(string us, string ini, int niv, float t, int st, int sc, int dr)
    {
        this.usuario = us;
        this.inicioRun = ini;
        this.nivel = niv;
        this.tiempo = t;
        this.salasTotales = st;
        this.salasCompletadas = sc;
        this.danoRecibido = dr;
    }
}

public class charController : MonoBehaviour
{
    //public float speed;
    Vector3 movimiento;
    public CharacterController cont;
    //Vector2 mousePosition;
    public Camera cam;
    public bool corriendo = false;
    public Animator animador;
    public gameManager gm;
    public GameObject arma;
    public GameObject cuerpo;
    public statsJugador stats;
    public float cooldownMelee=0;
    public GameObject trail;
    public UnityEngine.UI.Slider sliderMelee;
    public bool vivo = true;
    public updateCam salaActual;
    public int danoNivel = 0;
    // Start is called before the first frame update
    void Start()
    {
        //speed = stats.velocidad;
        cam = GameObject.Find("Camara").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (vivo)
        {
            sliderMelee.value = 1 - (cooldownMelee / stats.cooldownMelee);
            cuerpo.transform.localPosition = new Vector3(-0.08f, -0.5f, -0.15f);
            Plane playerPlane = new Plane(Vector3.up, transform.position);
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            float hitDist = 1.0f;
            if (playerPlane.Raycast(ray, out hitDist) && gm.InputEnable /*&& !(animador.GetCurrentAnimatorClipInfo(1)[0].clip.name == "Attack02" || animador.GetCurrentAnimatorClipInfo(1)[0].clip.name == "Attack01")*/)
            {
                Vector3 targetPoint = ray.GetPoint(hitDist);
                Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
                targetRotation.x = 0;
                targetRotation.z = 0;
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
            }

            movimiento.x = Input.GetAxisRaw("Horizontal");
            movimiento.z = Input.GetAxisRaw("Vertical");
            if ((movimiento.x != 0 || movimiento.z != 0) && !animador.GetBool("corriendo") && gm.InputEnable)
            {
                corriendo = true;
                animador.SetBool("corriendo", true);
            }
            else if ((movimiento.x == 0 && movimiento.z == 0) && animador.GetBool("corriendo") && gm.InputEnable)
            {
                corriendo = false;
                animador.SetBool("corriendo", false);
            }
            if (gm.InputEnable)
            {
                cont.Move(movimiento * stats.velocidad * Time.deltaTime);
            }
            if (cooldownMelee > 0)
            {
                cooldownMelee -= Time.deltaTime;
            }
            else { cooldownMelee = 0; }
        }
        if(transform.position.y > 0.6f)
        {
            transform.position = new Vector3(transform.position.x, 0.6f, transform.position.z);
        }
    }
    public void morir()
    {
        vivo = false;
        animador.SetTrigger("morir");
        cont.enabled = false;
        Collider[] cols = GetComponentsInChildren<Collider>();
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        foreach(Collider col in cols)
        {
            col.enabled = false;
        }
        //Debug.Log("Analytics : " + gm.identificadorMaq + "--" + "muerte");
        /*AnalyticsService.Instance.CustomData("muerteRun", new Dictionary<string, object>
                {
                    { "UserRun",gm.identificadorMaq},
                    { "nivelActual", GameObject.Find("dificultad").GetComponent<dificultadLineal>().nivelDificultad },
                });
        try
        {
            AnalyticsService.Instance.Flush();
        }
        catch
        {
        }
        //Debug.Log("Analytics : " + gm.usuario + "--" + "muerte");
        AnalyticsService.Instance.CustomData("muerteUsuario", new Dictionary<string, object>
                {
                    { "User",gm.usuario},
                    { "nivelActual", GameObject.Find("dificultad").GetComponent<dificultadLineal>().nivelDificultad },
                });
        try
        {
            AnalyticsService.Instance.Flush();
        }
        catch
        {
        }*/
        //---------------PruebaFirebaseNest--------------
        string id = gm.identificadorMaq.Replace('.', ',');
        id = id.Replace('/', ',');
        string[] ident = id.Split('|');
        dificultadLineal df = GameObject.Find("dificultad").GetComponent<dificultadLineal>();
        muerteStatic ms = new muerteStatic(ident[0], ident[1], df.nivelDificultad, gm.tiempoNivel, gm.salasActuales.contadorSalas + 1, gm.salasActuales.salasSuperadas, danoNivel);
        string jsonString = JsonConvert.SerializeObject(ms);
        StartCoroutine(muerte(jsonString));
        //-----------------------------------------------
        /*Debug.Log("muerteRun: " + Analytics.IsCustomEventEnabled("muerteRun"));
        AnalyticsResult anRes = Analytics.CustomEvent("muerteRun-" + gm.identificadorMaq + "-" + GameObject.Find("dificultad").GetComponent<dificultadLineal>().nivelDificultad);
        Debug.Log("analyticsResult muerteRun: " + anRes);
        Analytics.FlushEvents();
        Debug.Log("muerteUsuario: " + Analytics.IsCustomEventEnabled("muerteUsuario"));
        anRes = Analytics.CustomEvent("muerteUsuario-" + gm.usuario + "-" + GameObject.Find("dificultad").GetComponent<dificultadLineal>().nivelDificultad);
        Debug.Log("analyticsResult muerteUsuario: " + anRes);
        Analytics.FlushEvents();
        Debug.Log("se hizo analytics de muerte");*/
        Invoke("habilitarMenu", 1f);
    }
    IEnumerator muerte(string js)
    {
        UnityWebRequest uwr = new UnityWebRequest("https://pcg-nest.herokuapp.com/muerteStatic", "POST");
        byte[] xmlToSend = Encoding.UTF8.GetBytes(js);
        uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(xmlToSend);
        string cadenadeXML = Encoding.UTF8.GetString(xmlToSend);
        //Debug.Log(cadenadeXML);
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
    private void FixedUpdate()
    {
        if (vivo)
        {
            if (Input.GetKey(KeyCode.Mouse1) && !((animador.GetCurrentAnimatorClipInfo(1)[0].clip.name == "Clip1") || (animador.GetCurrentAnimatorClipInfo(1)[0].clip.name == "WalkForwardBattle") /*|| (animador.GetCurrentAnimatorClipInfo(1)[0].clip.name == "lanzar")*/) && cooldownMelee <= 0 && gm.InputEnable)
            {
                cooldownMelee = stats.cooldownMelee;
                trail.SetActive(true);
                animador.SetTrigger("atacar");
            }
            else if (Input.GetKey(KeyCode.Mouse0) && !((animador.GetCurrentAnimatorClipInfo(1)[0].clip.name == "Clip1") || (animador.GetCurrentAnimatorClipInfo(1)[0].clip.name == "lanzar")) && gm.InputEnable)
            {
                animador.SetTrigger("atacar2");
            }
        }
    }
    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("fuego"))
        {
            GetComponent<statsJugador>().recibirDano(other.GetComponent<datosFuego>().dano);
        }
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        {
            if (hit.transform.name.Contains("enemigoV1"))
            {
                stats.recibirDano(hit.transform.GetComponent<statsEnemigo>().danoMelee);
            }
            else if (hit.transform.name.Contains("enemigoV2"))
            {
                stats.recibirDano(hit.transform.GetComponent<statsEnemigo2>().danoMelee);
            }
            else if (hit.transform.name.Contains("enemigoV3"))
            {
                stats.recibirDano(hit.transform.GetComponent<statsEnemigo3>().danoMelee);
            }
            else if (hit.transform.name.Contains("enemigoV4"))
            {
                stats.recibirDano(hit.transform.GetComponent<statsEnemigo4>().danoMelee);
            }
        }
    }
    private void habilitarMenu()
    {
        gm.pausar();
    }
}
