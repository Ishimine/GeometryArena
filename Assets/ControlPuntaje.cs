using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPuntaje : MonoBehaviour {


    public int puntajeAct;
    public static ControlPuntaje instance;
    public delegate void trigger(int p);
    public trigger actualizacion;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    void Start ()
    {
        Inicializar();		
	}

    void Inicializar()
    {
        puntajeAct = 0;
    }
	

    void SumarPuntaje(int x)
    {
        puntajeAct += x;
        if (actualizacion != null) actualizacion(puntajeAct);
    }
}
