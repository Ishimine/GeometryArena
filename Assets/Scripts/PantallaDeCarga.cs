using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PantallaDeCarga : MonoBehaviour {

    public Image[] renders;


    private void Awake()
    {
        SelectorDeNiveles.instance.porcentajeDeCarga += ActualizarBarra;
        SelectorDeNiveles.instance.NivelCargado += Desactivar;
    }

    void ActualizarBarra(float f)
    {
        foreach(Image i in renders)
        {
            i.fillAmount = f;
        }
    }

    void Desactivar()
    {
        this.gameObject.SetActive(false);
    }
}
