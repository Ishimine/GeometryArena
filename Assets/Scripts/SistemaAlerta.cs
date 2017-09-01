using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistemaAlerta : MonoBehaviour
{
    public float distAlBorde;
    public float camRangoVertical;
    public float camRangoHorizontal;

    public int cantMarcadoresEnemigos = 10;
    public int cantMarcadoresAmenazas = 10;

    public static List<GameObject> amenazas = new List<GameObject>();
    public static List<GameObject> enemigos = new List<GameObject>();

    public GameObject marcadorAmenaza;
    public GameObject marcadorEnemigo;

    GameObject[] marcadoresAmenazas;
    GameObject[] marcadoresEnemigos;


    private void Awake()
    {
        CalcularRangoCamara();
        InicializacionDeMarcadores();
    }

    void CalcularRangoCamara()
    {
        camRangoVertical = Camera.main.orthographicSize;
        camRangoHorizontal = camRangoVertical * Screen.width / Screen.height;
        camRangoVertical -= distAlBorde;
        camRangoHorizontal -= distAlBorde;
    }

    void InicializacionDeMarcadores()
    {
        marcadoresAmenazas = new GameObject[cantMarcadoresAmenazas];
        marcadoresEnemigos = new GameObject[cantMarcadoresEnemigos];

        for (int i = 0; i < marcadoresAmenazas.Length; i++)
        {
            GameObject clone = Instantiate(marcadorAmenaza, this.transform);
            marcadoresAmenazas[i] = clone;
            clone.SetActive(false);
        }

        for (int i = 0; i < marcadoresEnemigos.Length; i++)
        {
            GameObject clone = Instantiate(marcadorEnemigo, this.transform);
            marcadoresEnemigos[i] = clone;
            clone.SetActive(false);
        }
    }


    public void Update()
    {
        PosicionarMarcadores(amenazas, marcadoresAmenazas);
        PosicionarMarcadores(enemigos, marcadoresEnemigos);
    }


    public void PosicionarMarcadores(List<GameObject> objetivos, GameObject[] marcadores)
    {
        int marki = 0;

        for(int i = 0;i < objetivos.Count;i++)
        {
            GameObject g = objetivos[i];

            if(EstaFueraDeVision(ref g))
            {
                Vector2 correccion = g.transform.position - Camera.main.transform.position;

                float x = g.transform.position.x - Camera.main.transform.position.x;
                if (Mathf.Abs(x) > camRangoHorizontal + distAlBorde)
                {
                    correccion.x = Mathf.Sign(x) * camRangoHorizontal;
                }

                float y = g.transform.position.y - Camera.main.transform.position.y;
                if (Mathf.Abs(y) > camRangoVertical + distAlBorde)
                {
                    correccion.y = Mathf.Sign(y) * camRangoVertical;
                }

                correccion += (Vector2)Camera.main.transform.position;

                float dist = Vector2.Distance(Camera.main.transform.position, g.transform.position);

                marcadores[i].transform.localScale = Vector2.Lerp(Vector2.one*2, Vector2.one/3,dist/40);


                Debug.DrawLine(marcadores[i].transform.position, g.transform.position,Color.yellow);
                if (marki >= marcadores.Length)
                    break;

                marcadores[marki].transform.position = correccion;
                marcadores[marki].SetActive(true);
                marki++;
            }
        }

        for(int i = marki; i < marcadores.Length; i++)
        {
            marcadores[i].SetActive(false);
        }       

    }

    public bool EstaFueraDeVision(ref GameObject g)
    {
        float x = Mathf.Abs(g.transform.position.x - Camera.main.transform.position.x);
        float y = Mathf.Abs(g.transform.position.y - Camera.main.transform.position.y);

        if ((x >= camRangoHorizontal) || (y >= camRangoVertical))
        {
            return true;
        }
        return false;
    }


    public static void AgregarAmenaza(GameObject g)
    {
        amenazas.Add(g);
    }

    public static void QuitarAmenaza(GameObject g)
    {
        amenazas.Remove(g);
    }


    public static void AgregarEnemigo(GameObject g)
    {
        enemigos.Add(g);
    }

    public static void QuitarEnemigo(GameObject g)
    {
        enemigos.Remove(g);
    }

}
