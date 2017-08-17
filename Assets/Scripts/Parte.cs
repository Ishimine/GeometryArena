using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parte : MonoBehaviour
{
    public Unidad unidad;

    public enum TipoParte { Defensivo, Ofensivo, Debil, Jugador }
    public TipoParte tipo;

    public float daño;
    Collider2D col;
    public delegate void Trigger(Collision2D col, Parte parte);
    public Trigger dañoDado;

    public delegate void TriggerDaño(float daño, Collision2D other);
    public TriggerDaño dañoRecibido;

    public FX_Impacto fx;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
        fx = GetComponent<FX_Impacto>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (unidad.dead || unidad.invulnerable) return;

        Parte otherParte = other.collider.gameObject.GetComponent<Parte>();
        if (otherParte == null) return;
        //Debug.Log("Other.collider.gameobject.name" + otherParte.name);

        switch (tipo)
        {
            case TipoParte.Defensivo:
                if (otherParte.tipo == TipoParte.Ofensivo)
                    AnimarChispaso();
                break;
            case TipoParte.Ofensivo:
                if (otherParte.tipo == TipoParte.Ofensivo)
                    AnimarChispaso();

                else if ((otherParte.tipo == TipoParte.Jugador) || (otherParte.tipo == TipoParte.Debil))
                {
                    Dañar(otherParte, other);
                }

                break;
            case TipoParte.Debil:

                break;
            case TipoParte.Jugador:
                if (otherParte.tipo == TipoParte.Debil)
                {
                    Dañar(otherParte, other);
                    unidad.FxImpacto();
                }

                break;
            default:
                break;
        }     
    }

    void AnimarChispaso()
    {
       // Debug.Log("Chispaso Animacion Faltante");
    }

    void Dañar(Parte p, Collision2D other)
    {
        //print(p.name);
        p.RecibirDaño(daño, other);
    }

    void RecibirDaño(float daño, Collision2D other)
    {
        if (unidad.dead || unidad.invulnerable) return;
        if (dañoRecibido != null) dañoRecibido(daño * other.relativeVelocity.sqrMagnitude / 1000, other);
        if (fx != null) fx.DestelloMuerte();
    }




}
