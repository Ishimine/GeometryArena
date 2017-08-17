using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotacionContinua : MonoBehaviour {


    public Vector3 direccion;
    public bool invertir = false;
    public bool encendido = true;

    public void Update()
    {
        if (!encendido)
            return;
        int i = 1;
        if (invertir) i = -1;
        transform.Rotate (direccion * Time.deltaTime *i);
    }
}
