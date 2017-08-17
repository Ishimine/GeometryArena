using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MenuNiveles : MonoBehaviour
{


    public bool usarTexto = true;
    public bool usarIndiceParaTexto = true;
    public bool usarBotonesPagina = true;

    public int BotonesPorPagina = 10;
    public GameObject botonPrefab;
    private BotonDeNivel[] botones;
    public int pagina;
    public int paginaMaxima = 10;

    public GameObject botonSigPagina;
    public GameObject botonAntPagina;

    public GameObject contenedorNiveles;
 

    void CrearBotones()
    {
        if (transform.childCount > 0)
            DestruirHijos();
        botones = new BotonDeNivel[BotonesPorPagina];

        for (int i = 0; i < BotonesPorPagina; i++)
        {
            GameObject clone = Instantiate<Object>(botonPrefab, contenedorNiveles.transform) as GameObject;
            clone.name = "Boton Nivel°" + (i + 1 + pagina*BotonesPorPagina);
            botones[i] = clone.GetComponent<BotonDeNivel>();
            botones[i].usarTexto = usarTexto;
            botones[i].usarIndiceParaTexto = usarIndiceParaTexto;
        }
    }

    void DestruirHijos()
    {
        int a = contenedorNiveles.transform.childCount-1;
        for (int i = a; i >= 0; i--)
        {
            DestroyImmediate(contenedorNiveles.transform.GetChild(i).gameObject);
        }
    }
    
    public void SigPagina()
    {
        pagina++;
        AsignarIndices();
        CheckBotonesPagina();
    }

    public void AntPagina()
    {
        pagina--;
        AsignarIndices();
        CheckBotonesPagina();
    }


    void AsignarIndices()
    {
        int indiceBase = pagina * BotonesPorPagina;
        for (int i = 0; i < BotonesPorPagina; i++)
        {
            botones[i].indiceNivel = i + 1 + indiceBase;
            botones[i].CheckText();
        }
    }


    /// <summary>
    /// Chequea que los botones Sig y Ant esten activos o inactivos de acuerdo al numero de pagina actual y al numero de paginas permitidas. Ejemplo: si estamos
    /// en la pagina 0, desaparecera el boton Ant, de la misma forma que si la Pagina maxima ex 10 y la pagina actual es 10, desaparecera el boton Siguiente
    /// </summary>
    void CheckBotonesPagina()
    {
        if(usarBotonesPagina)
        {
            if(pagina >= paginaMaxima)
                botonSigPagina.SetActive(false);
            else
                botonSigPagina.SetActive(true);
            if(pagina < 0)
                botonAntPagina.SetActive(false);
            else
                botonAntPagina.SetActive(true);
        }
        else
        {
            botonSigPagina.SetActive(false);
            botonAntPagina.SetActive(false);
        }
    }


    public void ActualizarBotones()
    {
        CrearBotones();
        AsignarIndices();
        CheckBotonesPagina();
    }


#if UNITY_EDITOR
    private void OnValidate()
    {
    }
#endif
}
