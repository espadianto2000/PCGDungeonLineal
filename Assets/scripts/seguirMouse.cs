using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class seguirMouse : MonoBehaviour
{
    public Canvas myCanvas;
    public GameObject TextoStat;
    public Sprite VidaMax;
    public Sprite Vida;
    public Sprite velocidad;
    public Sprite DanoMelee;
    public Sprite DanoRango;
    public Sprite VelocidadAtkRango;
    public Sprite CooldownMelee;
    public Sprite TamanoEspada;
    public Sprite RangoDist;
    public Sprite Knockback;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(myCanvas.transform as RectTransform, Input.mousePosition, myCanvas.worldCamera, out pos);
        transform.position = myCanvas.transform.TransformPoint(pos);*/
        //transform.position = Input.mousePosition + new Vector3((GetComponent<RectTransform>().sizeDelta.x)/2, (GetComponent<RectTransform>().sizeDelta.y)/2,0);
        transform.position = Input.mousePosition /*+ new Vector3(((GetComponent<RectTransform>().rect.width) / 2)+20, ((GetComponent<RectTransform>().rect.height) / 2)+10, 0)*/;
    }
    public void actualizar(GameObject obj)
    {
        statsRegalo stats = obj.GetComponent<statsRegalo>();
        for(int i = 1; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        if (stats.cambiarVidaMax != 0)
        {
            GameObject panel = Instantiate(TextoStat, transform);
            GameObject texto = panel.transform.GetChild(0).gameObject;
            if(stats.cambiarVidaMax<0)
            texto.GetComponent<TextMeshProUGUI>().text = "Vida max: " + stats.cambiarVidaMax;
            else { texto.GetComponent<TextMeshProUGUI>().text = "Vida max: +" + stats.cambiarVidaMax; }
            panel.transform.GetChild(1).GetComponent<RawImage>().texture = VidaMax.texture;
        }
        if (stats.cambiarVida != 0)
        {
            GameObject panel = Instantiate(TextoStat, transform);
            GameObject texto = panel.transform.GetChild(0).gameObject;
            if (stats.cambiarVida < 0)
                texto.GetComponent<TextMeshProUGUI>().text = "Vida: " + stats.cambiarVida;
            else { texto.GetComponent<TextMeshProUGUI>().text = "Vida: +" + stats.cambiarVida; }
            panel.transform.GetChild(1).GetComponent<RawImage>().texture = Vida.texture;
        }
        if(stats.cambiarVelocidad != 0)
        {
            GameObject panel = Instantiate(TextoStat, transform);
            GameObject texto = panel.transform.GetChild(0).gameObject;
            if (stats.cambiarVelocidad < 0)
                texto.GetComponent<TextMeshProUGUI>().text = "Velocidad: " + stats.cambiarVelocidad;
            else { texto.GetComponent<TextMeshProUGUI>().text = "Velocidad: +" + stats.cambiarVelocidad; }
            panel.transform.GetChild(1).GetComponent<RawImage>().texture = velocidad.texture;
        }
        if (stats.cambiarDanoMelee != 0)
        {
            GameObject panel = Instantiate(TextoStat, transform);
            GameObject texto = panel.transform.GetChild(0).gameObject;
            if (stats.cambiarDanoMelee < 0)
                texto.GetComponent<TextMeshProUGUI>().text = "Atk. Melee: " + stats.cambiarDanoMelee;
            else { texto.GetComponent<TextMeshProUGUI>().text = "Atk. Melee: +" + stats.cambiarDanoMelee; }
            panel.transform.GetChild(1).GetComponent<RawImage>().texture = DanoMelee.texture;
        }
        if (stats.cambiarDanoRango != 0)
        {
            GameObject panel = Instantiate(TextoStat, transform);
            GameObject texto = panel.transform.GetChild(0).gameObject;
            if (stats.cambiarDanoRango < 0)
                texto.GetComponent<TextMeshProUGUI>().text = "Atk. Rango: " + stats.cambiarDanoRango;
            else { texto.GetComponent<TextMeshProUGUI>().text = "Atk. Rango: +" + stats.cambiarDanoRango; }
            panel.transform.GetChild(1).GetComponent<RawImage>().texture = DanoRango.texture;
        }
        if (stats.cambiarVelocidadAtaqueRango != 0)
        {
            GameObject panel = Instantiate(TextoStat, transform);
            GameObject texto = panel.transform.GetChild(0).gameObject;
            if (stats.cambiarVelocidadAtaqueRango < 0)
                texto.GetComponent<TextMeshProUGUI>().text = "Vel. Atk Rango: " + stats.cambiarVelocidadAtaqueRango;
            else { texto.GetComponent<TextMeshProUGUI>().text = "Vel. Atk Rango: +" + stats.cambiarVelocidadAtaqueRango; }
            panel.transform.GetChild(1).GetComponent<RawImage>().texture = VelocidadAtkRango.texture;
        }
        if (stats.cambiarCooldownMelee != 0)
        {
            GameObject panel = Instantiate(TextoStat, transform);
            GameObject texto = panel.transform.GetChild(0).gameObject;
            if (stats.cambiarCooldownMelee < 0)
                texto.GetComponent<TextMeshProUGUI>().text = "Cooldown Melee: " + stats.cambiarCooldownMelee;
            else { texto.GetComponent<TextMeshProUGUI>().text = "Cooldown Melee: +" + stats.cambiarCooldownMelee; }
            panel.transform.GetChild(1).GetComponent<RawImage>().texture = CooldownMelee.texture;
        }
        if (stats.cambiarTamañoEspada != 0)
        {
            GameObject panel = Instantiate(TextoStat, transform);
            GameObject texto = panel.transform.GetChild(0).gameObject;
            if (stats.cambiarTamañoEspada < 0)
                texto.GetComponent<TextMeshProUGUI>().text = "Largo Espada: " + stats.cambiarTamañoEspada;
            else { texto.GetComponent<TextMeshProUGUI>().text = "Largo Espada: +" + stats.cambiarTamañoEspada; }
            panel.transform.GetChild(1).GetComponent<RawImage>().texture = TamanoEspada.texture;
        }
        if (stats.cambiarRangoDistancia != 0)
        {
            GameObject panel = Instantiate(TextoStat, transform);
            GameObject texto = panel.transform.GetChild(0).gameObject;
            if (stats.cambiarRangoDistancia < 0)
                texto.GetComponent<TextMeshProUGUI>().text = "Rango Dist.: " + stats.cambiarRangoDistancia;
            else { texto.GetComponent<TextMeshProUGUI>().text = "Rango Dist.: +" + stats.cambiarRangoDistancia; }
            panel.transform.GetChild(1).GetComponent<RawImage>().texture = RangoDist.texture;
        }
        if (stats.cambiarKnockbackMelee != 0)
        {
            GameObject panel = Instantiate(TextoStat, transform);
            GameObject texto = panel.transform.GetChild(0).gameObject;
            if (stats.cambiarKnockbackMelee < 0)
                texto.GetComponent<TextMeshProUGUI>().text = "Knockback: " + stats.cambiarKnockbackMelee;
            else { texto.GetComponent<TextMeshProUGUI>().text = "Knockback: +" + stats.cambiarKnockbackMelee; }
            panel.transform.GetChild(1).GetComponent<RawImage>().texture = Knockback.texture;
        }
        if (stats.recibirDano != 0)
        {
            GameObject panel = Instantiate(TextoStat, transform);
            GameObject texto = panel.transform.GetChild(0).gameObject;
            if (stats.recibirDano < 0)
                texto.GetComponent<TextMeshProUGUI>().text = "Vida: " + stats.recibirDano;
            else { texto.GetComponent<TextMeshProUGUI>().text = "Vida: +" + stats.recibirDano; }
            panel.transform.GetChild(1).GetComponent<RawImage>().texture = Vida.texture;
        }
    }
}
