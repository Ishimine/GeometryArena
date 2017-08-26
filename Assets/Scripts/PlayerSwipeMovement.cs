using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwipeMovement : Unidad {

   // public static PlayerSwipeMovement instance;
    public float multiplicadorGastoEnergia = 1;
    public GameObject referencia;

    public bool usarFuerzaEstatica;
    public float fuerza;
    bool frenando = false;

    ShakeControl shakeCam;

    public Vector2 freno = new Vector2(3,5);

    TouchControl tc;
    Collider2D col;
    

    new void Start ()
    {
        base.Start();
        Activar();
    }

    public override void Activar()
    {
        base.Activar();
        shakeCam = FindObjectOfType<ShakeControl>();
        tc = FindObjectOfType<TouchControl>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        tc.DireccionPotencia += Impulsar;
        tc.touchEstacionario += ActivarFreno;
        tc.touchOut += SoltarFreno;
        tc.touchOut += DesactivarReferencia;
        tc.DireccionEstacionaria += Mirar;
        if (ActVida != null) ActVida(vidaAct, vidaMax);
    }


    private void OnDestroy()
    {
        tc.DireccionPotencia -= Impulsar;
        tc.touchEstacionario -= ActivarFreno;
        tc.touchOut -= SoltarFreno;
        tc.touchOut -= DesactivarReferencia;
        tc.DireccionEstacionaria -= Mirar;
    }

    void ActualizarReferencia(float dist)
    {
        referencia.SetActive(true);
        referencia.transform.localScale = new Vector3(dist / 20, 1, 1);
    }

    void DesactivarReferencia()
    {
        referencia.SetActive(false);
    }

    bool GastarEnergia(ref float cant)
    {
        float aux = energiaAct - (cant * multiplicadorGastoEnergia);

        if (aux > 0)
        {
            energiaAct-= (cant * multiplicadorGastoEnergia);
            if(ActEnergia!= null) ActEnergia(energiaAct, energiaMax);
            return true;
        }
        else
        {
            cant = energiaAct;
            energiaAct = 0;
            if(ActEnergia!= null) ActEnergia(energiaAct, energiaMax);         
            return false;
        }
    }

    public override void RecibirDaño(float dañoRecibido, Collision2D other)
    {
        if ((invulnerable) || (dead)) return;

        base.RecibirDaño(dañoRecibido, other);
        invulnerable = true;
        StartCoroutine(TiempoInvulnerable(1f));
        shakeCam.ActivarShakeNormal(ShakeControl.FuerzaShake.Medio);

    }

    public override GameObject CrearTextoPop(Vector3 pos, float dañoRecibido)
    {
        GameObject x = base.CrearTextoPop(pos, dañoRecibido);
        x.GetComponent<TextMesh>().color = Color.red;
        return x;
    }

    void Impulsar(Vector2 dir, float f)
    {
        if (dead) return;


        if (usarFuerzaEstatica)
        {
            rb.AddForce(dir.normalized * fuerza, ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(dir.normalized * fuerza * f, ForceMode2D.Impulse);
        }
        rb.angularVelocity = 0;
        Mirar(dir);
    }

  

    void Mirar(Vector2 dir, float f)
    {
        if (dead) return;
        ActualizarReferencia(f);
        Mirar(dir);
    }
    void Mirar(Vector2 dir)
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;                                                    //Angulo de la rotacion
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    void ActivarFreno()
    {
        frenando = true;
    }

    void Frenar()
    {
        if(frenando)
        {
            rb.drag = freno[1];
            frenando = false;
        }
    }

    void SoltarFreno()
    {
        rb.drag = freno[0];
    }


    new void Update ()
    {
        if (dead) return;
        base.Update();
        Frenar();
    }

    override public void Muerto()
    {
        base.Muerto();
        GameController.instance.GameOver();
    }



    public override void FxImpacto()
    {
        shakeCam.ActivarShakeNormal(ShakeControl.FuerzaShake.Debil);
    }

    public override void FxImpactoRealizado()
    {
      //  throw new NotImplementedException();
    }

    public override void FxImpactoRealizado(Vector3 impactPos)
    {
        //throw new NotImplementedException();
    }
}
