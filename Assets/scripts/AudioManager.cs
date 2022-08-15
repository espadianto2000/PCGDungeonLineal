using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] audiosGameplay;
    public AudioClip[] audiosJefe;
    public AudioClip win;
    public AudioClip pause;
    public int estado = 0;
    public bool cambio=false;
    public bool pausa = false;
    public AudioSource p;
    // Update is called once per frame
    void Update()
    {
        if (!pausa)
        {
            if (estado == 1)
            {
                GetComponent<AudioSource>().loop = false;
                if (GetComponent<AudioSource>().clip == null || GetComponent<AudioSource>().time >= GetComponent<AudioSource>().clip.length-1)
                {
                    GetComponent<AudioSource>().clip = audiosGameplay[Random.Range(0, audiosGameplay.Length)];
                    GetComponent<AudioSource>().Play();
                }
            }
            else if (estado == 2)
            {
                GetComponent<AudioSource>().loop = false;
                if (GetComponent<AudioSource>().clip == null ||  GetComponent<AudioSource>().time >= GetComponent<AudioSource>().clip.length - 2)
                {
                    GetComponent<AudioSource>().clip = audiosJefe[Random.Range(0, audiosJefe.Length)];
                    GetComponent<AudioSource>().Play();
                }
            }
            else if (estado == 3)
            {
                if (GetComponent<AudioSource>().clip == null || GetComponent<AudioSource>().loop == false)
                {
                    GetComponent<AudioSource>().loop = true;
                    GetComponent<AudioSource>().clip = win;
                    GetComponent<AudioSource>().Play();
                }
            }
            if (cambio)
            {
                GetComponent<AudioSource>().Pause();
                GetComponent<AudioSource>().volume -= Time.deltaTime / 4f;
                if (GetComponent<AudioSource>().volume == 0)
                {
                    cambio = false;
                    GetComponent<AudioSource>().clip = null;
                }
            }
            else
            {
                if (!GetComponent<AudioSource>().isPlaying)
                {
                    GetComponent<AudioSource>().Play();
                }
                if (GetComponent<AudioSource>().volume < 0.1f)
                {
                    GetComponent<AudioSource>().volume += Time.deltaTime / 4f;
                }
                else if (GetComponent<AudioSource>().volume > 0.1f)
                {
                    GetComponent<AudioSource>().volume = 0.1f;
                }
            }
        }
    }
    public void activarGameplay()
    {
        estado = 1;
        cambio = true;
    }
    public void activarBoss()
    {
        estado = 2;
        cambio = true;
    }
    public void activarPausa()
    {
        p.enabled = true;
        pausa = true;
        GetComponent<AudioSource>().Pause();
        p.Play();
    }
    public void desactivarPausa()
    {
        p.Pause();
        p.time = 0;
        p.enabled = false;
        GetComponent<AudioSource>().Play();
        pausa = false;
    }
    public void activarWin()
    {
        estado = 3;
        cambio = true;
    }
}
