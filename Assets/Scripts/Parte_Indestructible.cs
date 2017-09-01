using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parte_Indestructible : Parte
{


    public override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

        Parte p = collision.gameObject.GetComponent<Parte>();
        if (p == null) return;

        if (p.tipo == tipoParte.Ofensivo)
        {
            ImpactoOfensivo();
        }
        else if(p.tipo != tipoParte.PuntoDebil)
        {
            ImpactoDefensivo();
        }
    }

    void ImpactoOfensivo()
    {

    }

    void ImpactoDefensivo()
    {

    }
    
}
