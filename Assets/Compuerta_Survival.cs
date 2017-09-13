using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compuerta_Survival : Compuerta {

    /// <summary>
    /// Tiempo entre unidades creadas
    /// </summary>
    public float spawnRate = 15;


    public override void ActivarCompuerta()
    {
        CrearUnidad();
        StartCoroutine(IniciarContador());
    }


    IEnumerator IniciarContador()
    {
        yield return new WaitForSeconds(spawnRate);
        ActivarUnidad();
        if(GameController.enJuego)
        {
            ActivarCompuerta();
        }
    }
}
