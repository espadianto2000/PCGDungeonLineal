using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class auraExplosion : MonoBehaviour
{
    public GameObject circle;
    public GameObject rune;
    public bool crecido = false;
    public GameObject player;
    public GameObject explosion;
    public bool decrecer = false;
    public GameObject danoArea;
    public int dano;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player");
    }

    // Update is called once per frame
    void Update()
    {
        circle.transform.rotation = Quaternion.Euler(circle.transform.rotation.eulerAngles + new Vector3(0, 0, Time.deltaTime * 75));
        rune.transform.rotation = Quaternion.Euler(rune.transform.rotation.eulerAngles - new Vector3(0, 0, Time.deltaTime * 75));
        if (!decrecer && circle.transform.localScale.x < 1)
        {
            circle.transform.localScale += new Vector3(Time.deltaTime, Time.deltaTime, Time.deltaTime) * 0.5f;
            rune.transform.localScale += new Vector3(Time.deltaTime, Time.deltaTime, Time.deltaTime) * 0.5f;
            if(circle.transform.localScale.x >= 1)
            {
                Invoke("explosionar", 0.6f);
                crecido = true;
            }
        }
        if (!crecido)
        {
            transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        }
        if (decrecer)
        {
            circle.transform.localScale -= new Vector3(Time.deltaTime, Time.deltaTime, Time.deltaTime);
            rune.transform.localScale -= new Vector3(Time.deltaTime, Time.deltaTime, Time.deltaTime);
        }
    }
    public void explosionar()
    {
        explosion.SetActive(true);
        danoArea.SetActive(true);
        Invoke("quitarArea", 0.5f);
        decrecer = true;
        Invoke("destruir", 1f);
    }
    public void destruir()
    {
        Destroy(gameObject);
    }
    public void quitarArea()
    {
        danoArea.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
            other.GetComponent<statsJugador>().recibirDano(dano);
        }
    }
}
