using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Misil_Tipo_A : Unidad
{
    public ForceMode2D TipoDeFuerza;
    Rigidbody2D rb;

   public bool activado = false;


    public Transform puntoPropulsion;




    new void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        target = FindObjectOfType<PlayerSwipeMovement>().transform;
    }

    new void Update()
    {
        if (!activado || target == null)
            return;

        MirarTarget();
        Propulsion();
    }


    

    void Propulsion()
    {
        //rb.AddForceAtPosition(puntoPropulsion.up * fPropulsion, puntoPropulsion.position, ForceMode2D.Force);
        rb.AddRelativeForce(Vector3.right * fPropulsion, TipoDeFuerza);
    }

    public override void FxImpacto()
    {
        Debug.Log("FxImpacto: " + gameObject.name);
        Destroy(gameObject);
    }

    public override void FxImpactoRealizado()
    {
        Destroy(this.gameObject);
       // throw new NotImplementedException();
    }

    public override void FxImpactoRealizado(Vector3 impactPos)
    {
       // throw new NotImplementedException();
    }
}
