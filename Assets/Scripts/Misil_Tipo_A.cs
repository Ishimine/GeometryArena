using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Misil_Tipo_A : Unidad
{

    public ForceMode2D TipoDeFuerza;

   public bool activado = false;


    public Transform puntoPropulsion;




    new void Awake()
    {
        rb = GetComponent<Rigidbody2D>();        
    }


    public void PropulsionInicial(float f, float espera)
    {
        if(rb == null) rb = GetComponent<Rigidbody2D>();
        activado = false;
        BuscarTarget();
        rb.AddRelativeForce(Vector3.right * f, ForceMode2D.Impulse);
        StartCoroutine(EsperaDeActivacion(espera));
    }

    IEnumerator EsperaDeActivacion(float espera)
    {
        yield return new WaitForSeconds(espera);
        activado = true;
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
