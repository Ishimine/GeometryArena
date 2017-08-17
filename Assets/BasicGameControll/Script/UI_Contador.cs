using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Contador : MonoBehaviour {

    [SerializeField]private float valorActual;
    
    public void Sumar(float i)
    {
        valorActual += i;
    }

    public float GetValorActua()
    {
        return valorActual;
    }

}
