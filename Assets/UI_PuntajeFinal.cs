using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PuntajeFinal : MonoBehaviour {

    Text txt;


    private void Awake()
    {
        txt = GetComponent<Text>();
        
    }

    // Use this for initialization
    private void Start()
    {
        GameController.instance.SesionJuegoIniciada += Activar;
    }

    void Activar()
    {
        ActualizarTxt(ControlPuntaje.instance.puntajeAct);
    }


    void ActualizarTxt(int x)
    {
        txt.text = x.ToString();
    }
}
