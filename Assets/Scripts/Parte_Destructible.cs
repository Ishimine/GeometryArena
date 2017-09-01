using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parte_Destructible : Parte {
    
    public AudioClip sfxImpactoNormal;
    public AudioClip sfxImpactoLetal;

    public int vidaAct = 3;
    public int vidaMax = 3;
    public Color colorDestello = Color.white;
    public Color colorMuerte = Color.black;

    
    public virtual void ImpactoNormal()
    {
        ReproducirSonido(sfxImpactoNormal);
        StartCoroutine(Destello(colorDestello, colorOriginal));
    }

    public virtual void ImpactoLetal()
    {
        ReproducirSonido(sfxImpactoLetal);
        StartCoroutine(Destello(colorDestello, colorMuerte));
    }



    public override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        Parte_Ofensivo p = collision.collider.gameObject.GetComponent<Parte_Ofensivo>();
        if(p != null)
        {
            RecibirDaño(p.daño);
        }
    }

    IEnumerator Destello(Color cDestello, Color cFinal)
    {
        Color cActual = render.color;
        float t = 0;
        do
        {
            cActual = Vector4.Lerp(cDestello, cFinal, t);
            render.color = cActual;
            t += Time.deltaTime / 2;
            yield return null;
        } while (t < 1);   
    }


    public virtual void RecibirDaño(int daño)
    {
        if (vidaAct <= 0 || unidad.invulnerable)
            return;
        vidaAct -= daño;
        if (vidaAct <= 0)
        {
            ImpactoLetal();
        }
        else
        {
            ImpactoNormal();
        }
    }
    
}
