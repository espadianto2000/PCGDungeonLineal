using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generarDistribucion : MonoBehaviour
{
    public float[,] matriz = new float[10,10];
    public float[,] acumulado1 = new float[10, 10];
    public float[,] acumulado2 = new float[10, 10];
    public float[,] acumulado3 = new float[10, 10];
    public float[,] acumulado4 = new float[10, 10];
    public float[,] mapeado = new float[10, 10];
    public GameObject[] obstaculos;
    public GameObject[] enemigos;
    public GameObject premio;
    public GameObject trampa;
    private string archivoGuardar = "datos.csv";

    // Start is called before the first frame update
    void Start()
    {
        //sw1.Start();
        for(int i = 0; i < 10; i++)
        {
            //string cadena = "";
            for(int j = 0; j < 10; j++)
            {
                matriz[i,j] = Random.Range(1, 20);
            }
        }
        //formar caminos en cruz
        /*int centroTempx = Random.Range(4, 5);
        int centroTempy = Random.Range(4, 5);
        findPathGreedy(centroTempx, centroTempy, 0, Random.Range(4, 5));
        findPathGreedy(centroTempx, centroTempy, 9, Random.Range(4, 5));
        findPathGreedy(centroTempx, centroTempy, Random.Range(4, 5),0);
        findPathGreedy(centroTempx, centroTempy, Random.Range(4, 5),9);
        mapeado[4, 0] = true;
        mapeado[5, 0] = true;
        mapeado[4, 9] = true;
        mapeado[5, 9] = true;
        mapeado[0, 4] = true;
        mapeado[0, 5] = true;
        mapeado[9, 4] = true;
        mapeado[9, 5] = true;*/
        //formar caminos entrelazados
        findPathGreedy(4, 0, 0, 5);
        findPathGreedy(4, 0, 5, 9);
        findPathGreedy(4, 0, 9, 5);
        findPathGreedy(5, 9, 0, 4);
        findPathGreedy(5, 9, 9, 4);
        findPathGreedy(0, 4, 9, 5);
        mapeado[4, 0] = 9;
        mapeado[5, 0] = 9;
        mapeado[4, 9] = 9;
        mapeado[5, 9] = 9;
        mapeado[0, 4] = 9;
        mapeado[0, 5] = 9;
        mapeado[9, 4] = 9;
        mapeado[9, 5] = 9;
        //sw1.Stop();
        if(transform.position == Vector3.zero)
        //if(true)
        {
            /*float t1 = Time.realtimeSinceStartup;
            generarElementos2(45, 5, 0);
            string fila = "";
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (mapeado[j, i] == 9)
                    {
                        fila += 1 + ",";
                    }
                    else fila += mapeado[j, i] + ",";
                }
                
                fila += "\n";
            }
            try
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@archivoGuardar, true))
                {
                    file.WriteLine(fila);
                }
            }
            catch(System.Exception ex)
            {
                Debug.Log(ex);
            }
            //UnityEngine.Debug.Log(fila);
            //UnityEngine.Debug.Log("----------");
            instanciarElementos();
            float t2 = Time.realtimeSinceStartup;
            Debug.Log("tiempo de algoritmo inSala principal: " + (t2 - t1));*/
        }
        //sw2.Start();

        //generarElementos2(30, 5, 5);

        //sw2.Stop();
        //UnityEngine.Debug.Log("tiempo de carga: " + sw2.ElapsedMilliseconds);

        /*
        string fila = "";
        for (int i=0; i < 10; i++)
        {
            for(int j = 0; j < 10; j++)
            {
                if (mapeado[i, j] == 9)
                {
                    fila += 1 + ",";
                }else fila += mapeado[i, j] + ",";

            }
            
            fila += "\n";
        }
        UnityEngine.Debug.Log(fila);
        UnityEngine.Debug.Log("----------");
        */

        //instanciarElementos();
    }


    void findPathSum(int xIni,int yIni,int xFin, int yFin)
    {
        //intento minimun path sum de 2 direcciones
        if(xIni > xFin)
        {
            acumulado1[xIni,yIni] = matriz[xIni,yIni];
            for(int x = xIni-1;x>xFin-1; x--)
            {
                acumulado1[x, yIni] = matriz[x + 1, yIni] + matriz[x,yIni];
            }
            for(int y = yIni+1; y < yFin + 1; y++)
            {
                acumulado1[xIni, y] = matriz[xIni, y-1] + matriz[xIni, y];
            }
        }
        else if(xIni < xFin)
        {
            acumulado1[xIni, yIni] = matriz[xIni, yIni];
            for (int x = xIni+1; x < xFin+1 ; x++)
            {
                if (acumulado1[x, yIni] != 0)
                {
                    acumulado1[x, yIni] = matriz[x - 1, yIni] + matriz[x, yIni];
                }
                
            }
            for (int y = yIni + 1; y < yFin + 1; y++)
            {
                acumulado1[xIni, y] = matriz[xIni, y + 1] + matriz[xIni, y];
            }
        }
        else if(xIni == xFin)
        {

        }
    }
    void findPathGreedy(int xIni, int yIni, int xFin, int yFin)
    {
        int direccionX = 0;
        int direccionY = 0;
        int x = xIni;
        int y = yIni;
        if (xIni < xFin) { direccionX = 1; } else direccionX = -1;
        if (yIni < yFin) { direccionY = 1; } else direccionY = -1;
        while (x != xFin && y != yFin)
        {
            if (matriz[x + direccionX, y] < matriz[x, y + direccionY]) { x = x + direccionX; mapeado[x, y] = 1; } else { y = y + direccionY; mapeado[x, y] = 1; }
        }
        while(x != xFin)
        {
            x = x + direccionX; mapeado[x, y] = 1;
        }
        while (y != yFin)
        {
            y = y + direccionY; mapeado[x, y] = 1;
        }
    }
    void generarElementos(int mxObs, int mxEnem, int mxTramp)
    {
        List<int> opciones = new List<int> { 2, 2, 3, 3, 4, 4, 5, 5};
        //ruta a puertas = 1
        //obstaculo = 3;
        //enemigo = 4;
        //trampa = 5;
        //premio = 6;
        //espacioVacio = 2
        int contObs = 0;
        int contEnem = 0;
        int contTramp = 0;
        bool premio = false;
        for(int x = 0; x <5; x++)
        {
            for(int y = 0; y < 10; y++)
            {
                if (mapeado[x, y] == 0)
                {
                    mapeado[x, y] = opciones[Random.Range(0, opciones.Count)];
                    if(mapeado[x,y] == 5){contTramp += 1;}
                    else if (mapeado[x, y] == 4) { contEnem += 1; }
                    else if (mapeado[x, y] == 3) { contObs += 1; }
                }
                if (contObs >= mxObs) { opciones.RemoveAll(item => item == 3); }
                if (contEnem >= mxEnem) { opciones.RemoveAll(item => item == 4); }
                if (contTramp >= mxTramp) { opciones.RemoveAll(item => item == 5); }

                if (mapeado[9 - x, 9 - y] == 0)
                {
                    mapeado[9-x, 9-y] = opciones[Random.Range(0, opciones.Count)];
                    if (mapeado[9 - x, 9 - y] == 5) { contTramp += 1; }
                    else if (mapeado[9 - x, 9 - y] == 4) { contEnem += 1; }
                    else if (mapeado[9 - x, 9 - y] == 3) { contObs += 1; }
                }
                if (contObs >= mxObs) { opciones.RemoveAll(item => item == 3); }
                if (contEnem >= mxEnem) { opciones.RemoveAll(item => item == 4); }
                if (contTramp >= mxTramp) { opciones.RemoveAll(item => item == 5); }
            }
        }
        while (!premio)
        {
            int randomX = Random.Range(0, 9);
            int randomY = Random.Range(0, 9);
            if (mapeado[randomX, randomY]==1){
                mapeado[randomX, randomY] = 6;
                premio = true;
            }
        }
    }

    public void generarElementos2(int mxObs, int mxEnem, int mxTramp)
    {
        
        //ruta a puertas = 1
        //ruta nueva = 2
        //obstaculos = 3,4
        //enemigos = 5
        //trampa = 6
        //premio = 7
        int contObs = 0;
        int contEnem = 0;
        int contTramp = 0;
        bool premio = false;
        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                if (mapeado[x, y] == 0)
                {
                    if (contObs < mxObs)
                    {
                        mapeado[x, y] = Random.Range(2, 4/*5*/);
                        if (mapeado[x, y] == 3 || mapeado[x, y] == 4) { contObs++; }
                    }
                    else { mapeado[x, y] = 2; }
                }
                if (mapeado[9 - x, 9 - y] == 0)
                {
                    if (contObs < mxObs)
                    {
                        mapeado[9 - x, 9 - y] = Random.Range(2, 4/*5*/);
                        if (mapeado[9 - x, 9 - y] == 3 || mapeado[x, y] == 4) { contObs++; }
                    }
                    else { mapeado[9 - x, 9 - y] = 2; }
                }
            }
        }
        for (int x = 1; x < 6; x++)
        {
            for(int y = 1; y < 6; y++)
            {
                if (mapeado[5 - x, 5 - y] == 2) {
                    try { if (mapeado[5 - x - 1, 5 - y] == 1 || mapeado[5 - x - 1, 5 - y] == 9) { mapeado[5 - x, 5 - y] = 1; } } catch { /*Debug.Log("no se puede buscar arriba de:" + (5 - x) + "," + (5 - y));*/ }
                    try { if (mapeado[5 - x + 1, 5 - y] == 1 || mapeado[5 - x + 1, 5 - y] == 9) { mapeado[5 - x, 5 - y] = 1; } } catch { /*Debug.Log("no se puede buscar abajo de:" + (5 - x) + "," + (5 - y));*/ }
                    try { if (mapeado[5 - x, 5 - y - 1] == 1 || mapeado[5 - x, 5 - y - 1] == 9) { mapeado[5 - x, 5 - y] = 1; } } catch { /*Debug.Log("no se puede buscar izquierda de:" + (5 - x) + "," + (5 - y));*/  }
                    try { if (mapeado[5 - x, 5 - y + 1] == 1 || mapeado[5 - x, 5 - y + 1] == 9) { mapeado[5 - x, 5 - y] = 1; } } catch { /*Debug.Log("no se puede buscar derecha de:" + (5 - x) + "," + (5 - y));*/ }
                }
                if (mapeado[4 + x, 4 + y] == 2)
                {
                    try { if (mapeado[4 + x - 1, 4 + y] == 1 || mapeado[4 + x - 1, 4 + y] == 9) { mapeado[4 + x, 4 + y] = 1; } } catch { /*Debug.Log("no se puede buscar arriba de:" + (4 + x) + "," + (4 + y));*/ }
                    try { if (mapeado[4 + x + 1, 4 + y] == 1 || mapeado[4 + x + 1, 4 + y] == 9) { mapeado[4 + x, 4 + y] = 1; } } catch { /*Debug.Log("no se puede buscar abajo de:" + (4 + x) + "," + (4 + y));*/  }
                    try { if (mapeado[4 + x, 4 + y - 1] == 1 || mapeado[4 + x, 4 + y - 1] == 9) { mapeado[4 + x, 4 + y] = 1; } } catch { /*Debug.Log("no se puede buscar izquierda de:" + (4 + x) + "," + (4 + y));*/ }
                    try { if (mapeado[4 + x, 4 + y + 1] == 1 || mapeado[4 + x, 4 + y + 1] == 9) { mapeado[4 + x, 4 + y] = 1; } } catch { /*Debug.Log("no se puede buscar derecha de:" + (4 + x) + "," + (4 + y));*/ }
                }
                if (mapeado[5 - x, 4 + y] == 2)
                {
                    try { if (mapeado[5 - x - 1, 4 + y] == 1 || mapeado[5 - x - 1, 4 + y] == 9) { mapeado[5 - x, 4 + y] = 1; } } catch { /*Debug.Log("no se puede buscar arriba de:" + (5 - x) + "," + (4 + y));*/ }
                    try { if (mapeado[5 - x + 1, 4 + y] == 1 || mapeado[5 - x + 1, 4 + y] == 9) { mapeado[5 - x, 4 + y] = 1; } } catch { /*Debug.Log("no se puede buscar abajo de:" + (5 - x) + "," + (4 + y));*/  }
                    try { if (mapeado[5 - x, 4 + y - 1] == 1 || mapeado[5 - x, 4 + y - 1] == 9) { mapeado[5 - x, 4 + y] = 1; } } catch { /*Debug.Log("no se puede buscar izquierda de:" + (5 - x) + "," + (4 + y));*/ }
                    try { if (mapeado[5 - x, 4 + y + 1] == 1 || mapeado[5 - x, 4 + y + 1] == 9) { mapeado[5 - x, 4 + y] = 1; } } catch { /*Debug.Log("no se puede buscar derecha de:" + (5 - x) + "," + (4 + y));*/ }
                }
                if (mapeado[4 + x, 5 - y] == 2)
                {
                    try { if (mapeado[4 + x - 1, 5 - y] == 1 || mapeado[4 + x - 1, 5 - y] == 9) { mapeado[4 + x, 5 - y] = 1; } } catch { /*Debug.Log("no se puede buscar arriba de:" + (4 + x) + "," + (5 - y));*/ }
                    try { if (mapeado[4 + x + 1, 5 - y] == 1 || mapeado[4 + x + 1, 5 - y] == 9) { mapeado[4 + x, 5 - y] = 1; } } catch { /*Debug.Log("no se puede buscar abajo de:" + (4 + x) + "," + (5 - y));*/  }
                    try { if (mapeado[4 + x, 5 - y - 1] == 1 || mapeado[4 + x, 5 - y - 1] == 9) { mapeado[4 + x, 5 - y] = 1; } } catch { /*Debug.Log("no se puede buscar izquierda de:" + (4 + x) + "," + (5 - y));*/ }
                    try { if (mapeado[4 + x, 5 - y + 1] == 1 || mapeado[4 + x, 5 - y + 1] == 9) { mapeado[4 + x, 5 - y] = 1; } } catch { /*Debug.Log("no se puede buscar derecha de:" + (4 + x) + "," + (5 - y));*/ }
                }
            }
        }
        while (!premio)
        {
            int randomX = Random.Range(0, 9);
            int randomY = Random.Range(0, 9);
            if (mapeado[randomX, randomY] == 1)
            {
                mapeado[randomX, randomY] = 7;
                premio = true;
            }
        }
        while (contEnem < mxEnem)
        {
            int randomX = Random.Range(0, 9);
            int randomY = Random.Range(0, 9);
            if (mapeado[randomX, randomY] == 1)
            {
                mapeado[randomX, randomY] = 5;
                contEnem++;
            }
        }
        while (contTramp < mxTramp)
        {
            int randomX = Random.Range(0, 9);
            int randomY = Random.Range(0, 9);
            if (mapeado[randomX, randomY] == 1)
            {
                mapeado[randomX, randomY] = 6;
                contTramp++;
            }
        }
        
    }
    public void instanciarElementos(dificultadLineal dl)
    {
        float posXIni = transform.position.x - 4.5f;
        float posZIni = transform.position.z + 4.5f;
        int contEnem = 0;
        for(int x = 0; x < 10; x++)
        {
            for(int y = 0; y < 10; y++)
            {
                if(mapeado[x,y]==3|| mapeado[x, y] == 4)
                {
                    GameObject obs = Instantiate(obstaculos[Random.Range(0, obstaculos.Length)], new Vector3(posXIni + x, 0, posZIni - y), Quaternion.identity);
                    obs.transform.SetParent(transform);
                    //instanciamos obstaculos (rocas)
                }
                else if(mapeado[x, y] == 5)
                {
                    int ordenEnemigo = Random.Range(0, enemigos.Length);
                    GameObject enem = Instantiate(enemigos[ordenEnemigo], new Vector3(posXIni + x, 0.5f, posZIni - y), Quaternion.identity);
                    switch (ordenEnemigo)
                    {
                        case 0:
                            enem.GetComponent<statsEnemigo>().vidaMax = Random.Range(dl.stEn1.vidaMax, dl.stEn12.vidaMax);
                            enem.GetComponent<statsEnemigo>().velocidad = Random.Range(dl.stEn1.velocidad, dl.stEn12.velocidad);
                            enem.GetComponent<statsEnemigo>().danoMelee = dl.stEn1.danoMelee;
                            break;
                        case 1:
                            enem.GetComponent<statsEnemigo2>().vidaMax = Random.Range(dl.stEn2.vidaMax, dl.stEn22.vidaMax);
                            enem.GetComponent<statsEnemigo2>().velocidad = Random.Range(dl.stEn2.velocidad, dl.stEn22.velocidad);
                            enem.GetComponent<statsEnemigo2>().danoMelee = dl.stEn2.danoMelee;
                            enem.GetComponent<statsEnemigo2>().danoRango = dl.stEn2.danoRango;
                            enem.GetComponent<statsEnemigo2>().velocidadAtaque = Random.Range(dl.stEn2.velocidadAtaque, dl.stEn22.velocidadAtaque);
                            enem.GetComponent<statsEnemigo2>().velocidadProyectil = Random.Range(dl.stEn2.velocidadProyectil, dl.stEn22.velocidadProyectil);
                            enem.GetComponent<statsEnemigo2>().rango = Random.Range(dl.stEn2.rango, dl.stEn22.rango);
                            break;
                        case 2:
                            enem.GetComponent<statsEnemigo3>().vidaMax = Random.Range(dl.stEn3.vidaMax, dl.stEn32.vidaMax);
                            enem.GetComponent<statsEnemigo3>().velocidad = Random.Range(dl.stEn3.velocidad, dl.stEn32.velocidad);
                            enem.GetComponent<statsEnemigo3>().danoMelee = dl.stEn3.danoMelee;
                            enem.GetComponent<statsEnemigo3>().velocidadCrecimiento = Random.Range(dl.stEn3.velocidadCrecimiento, dl.stEn32.velocidadCrecimiento);
                            enem.GetComponent<statsEnemigo3>().cooldownCuracion = Random.Range(dl.stEn3.cooldownCuracion, dl.stEn32.cooldownCuracion);
                            enem.GetComponent<statsEnemigo3>().curacion = Mathf.Round(Random.Range(dl.stEn3.curacion, dl.stEn32.curacion)*100f)/100f;
                            break;
                        case 3:
                            enem.GetComponent<statsEnemigo4>().vidaMax = Random.Range(dl.stEn4.vidaMax, dl.stEn42.vidaMax);
                            enem.GetComponent<statsEnemigo4>().velocidad = Random.Range(dl.stEn4.velocidad, dl.stEn42.velocidad);
                            enem.GetComponent<statsEnemigo4>().danoMelee = dl.stEn4.danoMelee;
                            enem.GetComponent<statsEnemigo4>().cooldownAtaque = Random.Range(dl.stEn4.cooldownAtaque, dl.stEn42.cooldownAtaque);
                            break;
                    }
                    contEnem++;
                    enem.transform.SetParent(transform); 
                    transform.GetComponent<updateCam>().enemigosInstanciados.Add(enem);
                    //instanciamos enemigo
                }
                else if (mapeado[x, y] == 6)
                {
                    //instanciamos trampa
                    GameObject tramp = Instantiate(trampa, new Vector3(posXIni + x, 0, posZIni - y), Quaternion.identity);
                    GetComponent<updateCam>().trampas.Add(tramp);
                }
                else if (mapeado[x, y] == 7)
                {
                    int r = Random.Range(0, 100);
                    if(r <=100 && r > dl.probPremio)
                    {
                        GameObject prem = Instantiate(premio, new Vector3(posXIni + x, 0.5f, posZIni - y), Quaternion.identity);
                        prem.transform.SetParent(transform);
                        transform.GetComponent<updateCam>().premio = prem;
                        prem.transform.rotation = Quaternion.Euler(90, 0, 180);
                        GetComponent<updateCam>().premio = prem;
                        prem.GetComponent<statsRegalo>().pseudoStart();
                    }
                    //instanciamos premio posible
                }
            }
        }
    }
    public void instanciarPremio()
    {
        float posXIni = transform.position.x - 4.5f;
        float posZIni = transform.position.z + 4.5f;
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                if (mapeado[x, y] == 7)
                {
                    GameObject prem = Instantiate(premio, new Vector3(posXIni + x, 0.5f, posZIni - y), Quaternion.identity);
                    prem.transform.SetParent(transform);
                    transform.GetComponent<updateCam>().premio = prem;
                    prem.transform.rotation = Quaternion.Euler(90, 0, 180);
                    GetComponent<updateCam>().premio = prem;
                    prem.GetComponent<statsRegalo>().pseudoStart(4);
                }
            }
        }
    }
}
