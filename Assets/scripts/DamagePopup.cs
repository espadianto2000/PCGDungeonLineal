using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{

    public GameObject text;
    public float lifetime = 0.5f;
    public float dist = 3f;

    public Vector3 iniPos;
    public Vector3 targetPos;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(90,0,0);
        float direction = Random.rotation.eulerAngles.z;
        iniPos = new Vector3(transform.position.x, transform.position.y + 3f, transform.position.z);
        targetPos = iniPos + new Vector3(0, dist, 0);
        transform.localScale = Vector3.zero;
        Invoke("delete",0.4f);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        float fraction = lifetime / 2;

        if(timer > lifetime)
        {
            Destroy(gameObject);
        }
        else if (timer > fraction) { text.GetComponent<TextMeshProUGUI>().color = Color.Lerp(text.GetComponent<TextMeshProUGUI>().color, Color.clear, (timer - fraction) / (lifetime - fraction)); }
        transform.position = iniPos;
        //transform.position = Vector3.Lerp(iniPos, targetPos, Mathf.Sin(timer/lifetime));
        transform.localScale = Vector3.Lerp(new Vector3(0.0004f, 0.0004f, 0.0004f), new Vector3(0.002f,0.002f,0.002f), Mathf.Sin(timer / lifetime));
    }
    public void setDamageValue(float damage)
    {
        text.GetComponent<TextMeshProUGUI>().text = damage.ToString();
    }
    private void delete()
    {
        Destroy(gameObject);
    }
}
