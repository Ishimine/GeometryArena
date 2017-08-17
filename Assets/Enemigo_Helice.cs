using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo_Helice : Unidad
{
    public float velMov = 5;


    private new void Awake()
    {
        base.Awake();
        if(target == null) BuscarTarget();
    }


    

    public override void FxImpacto()
    {
    //    throw new NotImplementedException();
    }

    public override void FxImpactoRealizado()
    {
    //    throw new NotImplementedException();
    }

    public override void FxImpactoRealizado(Vector3 impactPos)
    {
    //    throw new NotImplementedException();
    }

   
    // Update is called once per frame
    new void Update()
    {
        if (target == null || dead)
            return;
        MirarTarget();
        Avanzar();
    }


    void Avanzar()
    {
        transform.Translate(Vector3.right * velMov * Time.deltaTime);
    }
}
