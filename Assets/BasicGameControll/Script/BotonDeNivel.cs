using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BotonDeNivel : MonoBehaviour {

    public bool usarTexto;
    public bool usarIndiceParaTexto;
    public Text txt;
    public int indiceNivel = 1; 


    public void CargarNivel()
    {
        SelectorDeNiveles.CargarNivel(indiceNivel);
    }

    public void CargarNivelAnterior()
    {
        SelectorDeNiveles.CargarNivelAnterior();
    }

    public void CargarNivelSiguiente()
    {
        SelectorDeNiveles.CargarNivelSiguiente();
    }

    public void Restart()
    {
        SelectorDeNiveles.Restart();
    }

    public void CargarMenu()
    {
        SelectorDeNiveles.CargarMenu();
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        CheckText();
    }
#endif

    public void CheckText()
    {
        if (usarTexto)
        {
            if(txt!= null) txt.gameObject.SetActive(true);
            if(usarIndiceParaTexto)
            {
                if (txt != null) txt.text = indiceNivel.ToString();
            }
        }
        else if (txt != null)
            txt.gameObject.SetActive(false);
    }

}
