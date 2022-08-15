using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossController2 : MonoBehaviour
{
    public GameObject player;
    public float dirx;
    public float dirZ;
    public Vector3 posCentral;
    public float rotacionY = 0;
    public int contadorRebotes=0;
    public statsBoss2 stats;
    void Start()
    {
        transform.rotation = Quaternion.Euler(-90,0,0);
        transform.position = new Vector3(transform.position.x,1.15f,transform.position.z);
        posCentral = transform.position;
        dirx = Random.Range(3, 7);
        dirZ = Random.Range(3, 7);
        switch(Random.Range(1, 3))
        {
            case 1:
                dirx = dirx * -100;
                break;
            case 2:
                dirZ = dirx * 100;
                break;
        }
        switch (Random.Range(1, 3))
        {
            case 1:
                dirx = dirZ * -100;
                break;
            case 2:
                dirZ = dirZ * 100;
                break;
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (player == null)
        {
            try
            {
                player = GameObject.Find("player");
            }
            catch
            {

            }
        }
        rotacionY += Time.fixedDeltaTime * 100;
        transform.rotation = Quaternion.Euler(-90, rotacionY, 0);
        
        if (transform.position.x > posCentral.x + 4.75f)
        {
            dirx = -1 * Mathf.Abs(dirx);
            transform.position = new Vector3(posCentral.x + 4.5f, 1.2f, transform.position.z);
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            contadorRebotes++;
        }
        else if (transform.position.x < posCentral.x - 4.75f)
        {
            dirx = Mathf.Abs(dirx);
            transform.position = new Vector3(posCentral.x - 4.5f, 1.2f, transform.position.z);
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            contadorRebotes++;
        }
        else transform.position = new Vector3(transform.position.x, 1.2f, transform.position.z);
        if (transform.position.z > posCentral.z + 4.75f)
        {
            dirZ = -1 * Mathf.Abs(dirZ);
            transform.position = new Vector3(transform.position.x, 1.2f, posCentral.z + 4.5f);
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            contadorRebotes++;
        }
        else if (transform.position.z < posCentral.z - 4.75f)
        {
            dirZ = Mathf.Abs(dirZ);
            transform.position = new Vector3(transform.position.x, 1.2f, posCentral.z - 4.5f);
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            contadorRebotes++;
        }
        if (contadorRebotes >= stats.rebotesMax)
        {
            contadorRebotes = 0;
            Vector3 newDir = player.transform.position - transform.position;
            dirx = newDir.normalized.x * 1000;
            dirZ = newDir.normalized.z * 1000;
        }
        if (player != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x + dirx, transform.position.y, transform.position.z + dirZ), (stats.velocidad + ((1 - (stats.vida / stats.vidaMax)) * stats.velocidadExtra)) * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("obstaculo"))
        {
            Destroy(other.gameObject);
        }
        if (other.CompareTag("proyectilJugador"))
        {
            GetComponent<statsBoss2>().recibirDano(player.GetComponent<statsJugador>().danoRango, other.transform.position, 1);
            Destroy(other.gameObject);
        }
        if (other.transform.CompareTag("sword") && player.GetComponentInChildren<Animator>().GetCurrentAnimatorClipInfo(1)[0].clip.name == "Clip1")
        {
            if (GetComponent<statsBoss2>().vulnerable)
            {
                GetComponent<statsBoss2>().vulnerable = false;
                GetComponent<statsBoss2>().recibirDano(player.GetComponent<statsJugador>().danoMelee, other.GetComponent<atacando>().player.transform.position, 0);
            }
        }
        if (other.CompareTag("player") && GetComponent<statsBoss2>().vida > 0)
        {
            other.GetComponent<statsJugador>().recibirDano(GetComponent<statsBoss2>().danoMelee);
        }
    }
}
