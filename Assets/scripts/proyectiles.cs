using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class proyectiles : MonoBehaviour
{
    // Start is called before the first frame update
    public statsJugador stats;
    public GameObject proyectil;
    public Camera cam;
    public GameObject player;
    public void Start()
    {
        cam = GameObject.Find("Camara").GetComponent<Camera>();
    }
    public void lanzarProyectil()
    {
        Plane playerPlane = new Plane(Vector3.up, player.transform.position);
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        float hitDist = 0f;
        Vector3 targetPoint=new Vector3(0,0,0);
        if (playerPlane.Raycast(ray, out hitDist))
        {
            targetPoint = ray.GetPoint(hitDist);
        }
        var proy = Instantiate(proyectil, transform);
        proy.transform.localPosition = new Vector3(-0.45f, 0.8f, 0.65f);
        proy.transform.parent = null;
        proy.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        proy.transform.rotation = Quaternion.identity;
        proy.GetComponent<proyectil>().destino = targetPoint;
        proy.GetComponent<proyectil>().tiempoVida = stats.rangoRango;
        proy.SetActive(true);
    }
}
