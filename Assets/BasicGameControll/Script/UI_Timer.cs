using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_Timer : MonoBehaviour {

    public bool activado = false;
    public float tiempoActual = 0;
    public Text txt;

    void Awake()
    {
        SelectorDeNiveles.instance.NivelCargado += ResetearTiempo;
    }
    private void OnDestroy()
    {
        SelectorDeNiveles.instance.NivelCargado -= ResetearTiempo;
    }

    public void IniciarTimer()
    {
        ResetearTiempo();
        activado = true;
    }

    public void ResetearTiempo()
    {
        tiempoActual = 0;
    }

    public void ContinuarTimer()
    {
        activado = true;
    }

    public void PausarTimer()
    {
        activado = false;
    }

    public void SumarTiempo(float i)
    {
        tiempoActual += i;
    }

    void ActualizarTimer()
    {
        tiempoActual += Time.deltaTime;
    }

    private void Update()
    {
        txt.text = tiempoActual.ToString("0.0");
        if (activado)
        {
            ActualizarTimer();
        }
    }
}
