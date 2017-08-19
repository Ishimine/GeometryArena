using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Enemigo_LanzaMisiles : Unidad
{
    public Vector2 limites = new Vector2(50,50);

    public GameObject marca;
    public float area = 5;
    public Vector3 posObj;
    public float tActivacionMisiles = .5f;
    public float fuezaDisparo;
    public GameObject proyectil;
    public Transform[] puntosDeDisparo;

    public float distMinEscape = 2;

    [FormerlySerializedAs("Velocidad Avance")] public float velMov = 2;


    private new void Awake()
    {
        base.Awake();
        StartCoroutine(EsperaActivacion());
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


    new void Update()
    {
        if (target == null || dead) return;
        base.Update();
        MirarTarget();
        Avanzar(posObj);

        if((Vector3.Distance(posObj, target.position) < distMinEscape)
             &&
            (Vector3.Distance(transform.position, target.position) < distMinEscape))
        {
            BuscarPosicionObjetivo();
        }

        if (energiaAct >= 100)
        {
            DispararMisiles();
            energiaAct = 0;
        }
    }


    void BuscarPosicionObjetivo()
    {
        posObj = new Vector2(UnityEngine.Random.Range(-limites.x, limites.x), UnityEngine.Random.Range(-limites.y, limites.y));

        /*
        float x = UnityEngine.Random.Range(-1, 1.1f);
        posObj =  target.position + new Vector3(x, Mathf.Sin(x), 0) * area;

        if (Mathf.Abs(posObj.x) > limites.x)
        {
            posObj.x = limites.x * Mathf.Sign(posObj.x);
        }
        if (Mathf.Abs(posObj.y) > limites.y)
        {
            posObj.y = limites.y * Mathf.Sign(posObj.y);
        }*/

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
