using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo_Javalina : Unidad {

    public float tMinimo = 2;
    public float tMaximo = 6;
    public bool usarReferencia;
    public GameObject referencia;
    public float rangoDeDispersion = 10;
    public float fEmbestida = 200;
    public float velGiro = 1;
    public float velCargaPreDisparo = 1;

    public SpriteRenderer refRender;

    public Color luzBajaIntensidad;
    public Color luzAltaIntensidad;

    public Vector3 luzCorta;
    public Vector3 luzLarga;

    /// <summary>
    /// El primer valor es cuando esta activado y el segundo desactivado y durante la carga
    /// </summary>
    public Vector2 drag = new Vector2(2, 5);


    public Color preparado = Color.red;
    public Color prendido = Color.white;
    public Color apagado = Color.gray;
    
    new void Start()
    {
        base.Start();
        StartCoroutine(EsperaInicial());
        //Debug.Log("Javalina Lista");
    }

    IEnumerator EsperaInicial()
    {
        yield return new WaitForSeconds(2f);

        //target = PlayerSwipeMovement.instance.gameObject;
        BuscarTarget();
        Preparar();
    }

    new void Update()
    {
        base.Update();
        if(Input.GetKeyDown(KeyCode.K))
        {
            Preparar();
        }
    }


    void Preparar()
    {

        rb.drag = drag[1];
        StopAllCoroutines();
        StartCoroutine(Esperar(UnityEngine.Random.Range(tMinimo, tMaximo)));

    }

    IEnumerator Apuntar()
    {

        Vector2 dir = target.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;                                                    //Angulo de la rotacion
        Quaternion rotObjetivo = Quaternion.AngleAxis(angle + UnityEngine.Random.Range(-rangoDeDispersion, rangoDeDispersion), Vector3.forward);
        Quaternion rotOriginal = transform.rotation;
        float t = 0;
        if (usarReferencia) referencia.SetActive(true);
        referencia.transform.localScale = Vector3.zero;


        Vector3 escalaInicial = new Vector3(64, 2, 2);

        referencia.transform.localScale = escalaInicial;

        if (usarReferencia) referencia.transform.localScale = escalaInicial;

        refRender.color = luzAltaIntensidad;


        while (t < 1)
        {
            if (!GameController.enPausa)
            {
                // print(t);
                transform.rotation = Quaternion.Lerp(rotOriginal, rotObjetivo, t);
                t += Time.deltaTime * velGiro;
                spriteEnergia.color = Color.Lerp(prendido, preparado, t);

                if (usarReferencia) referencia.transform.localScale = Vector3.Lerp(luzCorta, luzLarga, t);

                //  if (usarReferencia) refRender.color = Color.Lerp(luzBajaIntensidad, luzAltaIntensidad, t);
            }
            yield return null;
        }

        t = 0;
        Vector3 escalaObjetivo = new Vector3(64, .2f, 2);

        while (t < 1)
        {
            if (!GameController.enPausa)
            {
                t += Time.deltaTime * velCargaPreDisparo;
                if (usarReferencia)
                {
                    if (t < .75f)
                    {
                        referencia.transform.localScale = Vector3.Lerp(escalaInicial, escalaObjetivo, t / .75f);
                    }
                    else
                    {
                        referencia.transform.localScale = Vector3.Lerp(escalaInicial, new Vector3(64, .4f, 2),t/.25f);                        
                        if(t<.85)                        
                            refRender.color = Color.white;                        
                        else if(t<.95)                       
                            refRender.gameObject.SetActive(false);                       
                        else                       
                            refRender.gameObject.SetActive(true);
                       
                    }
                }
            }
            yield return null;
        }
        if (usarReferencia) referencia.SetActive(false);
        Atacar();
        Preparar();
    }

    IEnumerator Esperar(float x)
    {
        float i = 0;
        float t = 0;
        referencia.SetActive(true);
        refRender.color = luzAltaIntensidad;
        while (i < x)
        {
            if (!GameController.enPausa)
            {
                i += Time.deltaTime;
                t = i / x;
                spriteEnergia.color = Vector4.Lerp(apagado, prendido, t);

                referencia.transform.localScale = Vector3.Lerp(new Vector3(0, 2, 2), luzCorta, t);



            }
            yield return null;
        }

        yield return new WaitForSeconds(1f);
        StartCoroutine(Apuntar());
    }

    void Atacar()
    {
        rb.drag = drag[0];
        rb.AddForce(transform.right * fEmbestida, ForceMode2D.Impulse);        
    }

    public override void FxImpacto()
    {

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
