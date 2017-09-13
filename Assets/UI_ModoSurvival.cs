using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class UI_ModoSurvival : MonoBehaviour {


    public Text txtPuntaje;


    
	// Use this for initialization
	void Start ()
    {
        ControlPuntaje.instance.actualizacion += ActualizarTxtPuntaje;

    }
	
	void ActualizarTxtPuntaje(int x)
    {
        txtPuntaje.text = x.ToString();
    }
}
