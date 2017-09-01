using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Enemigo_LanzaMisiles : Unidad
{
    public Vector2 limites = new Vector2(50,50);
    public GameObject marca;
    public float tActivacionMisiles = .5f;
    public float fuezaDisparo;
    public GameObject proyectil;
    public Transform[] puntosDeDisparo;
    public float radioDePeligro = 20;
    public float radioObjetivo = 40;

    public float distMinEscape = 2;    


    private new void Awake()
    {
        base.Awake();
    }

    public override void Activar()
    {
        base.Activar();
        BuscarTarget();
    }


    void DispararMisiles()
    {
        foreach (Transform t in puntosDeDisparo)
        {
            DispararMisil(t,fuezaDisparo);
        }
    }

    void DispararMisil(Transform lugar, float fuerza)
    {
        GameObject clone = Instantiate(proyectil, lugar.position, lugar.rotation);

        Misil_Tipo_A m = clone.GetComponent<Misil_Tipo_A>();
        if(m != null)
        {
            m.PropulsionInicial(fuerza, tActivacionMisiles);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(posObj, radioDePeligro);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(posObj, radioObjetivo);
    }

    new void Update()
    {
        if (target == null || dead) return;
        base.Update();
        MirarTarget();
        //Avanzar(posObj);


        if (energiaAct >= 100)
        {
            DispararMisiles();
            energiaAct = 0;
        }
    }


    public override void CalcularDir()
    {
        float dist = Vector2.Distance(posObj, transform.position);
        if (dist > radioObjetivo)
        {
            dir = (posObj - (Vector2)transform.position).normalized;
        }
        else if (dist < radioDePeligro)
        {
            dir = ((Vector2)transform.position - posObj).normalized;
        }
        else
        {
            dir = this.transform.up;
        }

    }


    void BuscarPosicionObjetivo()
    {
        posObj = new Vector2(UnityEngine.Random.Range(-limites.x, limites.x), UnityEngine.Random.Range(-limites.y, limites.y));

      if (marca != null)   marca.transform.position = posObj;
    }

    void Avanzar(Vector3 pOjb)
    {
         Vector3 dif = pOjb - transform.position;
        rb.AddForce(dif.normalized * velMov, ForceMode2D.Impulse);

       // transform.Translate(dif.normalized * velMov * Time.deltaTime);
    }


    void Avanzar()
    {
        transform.Translate(Vector3.right * velMov * Time.deltaTime);
    }

    public override void FxImpacto()
    {
       // throw new NotImplementedException();
    }

    public override void FxImpactoRealizado()
    {
       // throw new NotImplementedException();
    }

    public override void FxImpactoRealizado(Vector3 impactPos)
    {
       // throw new NotImplementedException();
    }
}
