using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parte_Player : Parte_PuntoDebil
{

    public int dañoActual = 1;

    public override void ImpactoNormal()
    {
        base.ImpactoNormal();
        unidad.RecibirDaño();
    }

    public override void ImpactoLetal()
    {
        base.ImpactoLetal();
    }

    public new void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        Parte_PuntoDebil p = collision.collider.gameObject.GetComponent<Parte_PuntoDebil>();
        if(p != null)
        {   
            p.RecibirDaño(dañoActual);
        }
    }
}
