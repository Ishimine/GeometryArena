using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unidad : MonoBehaviour
{

    public bool dead = false;
    public bool invulnerable = false;

    public bool knockBack = true;
    public float knockBackForce = 10;

    public bool usarVidaSimple = true;
    


    public float velRecargaEnergia;
    public float vidaAct;
    public float vidaMax = 100;

    public float energiaAct;
    public float energiaMax = 100;

    public Parte[] partesDebiles;

    public SpriteRenderer spriteEnergia;

    public delegate void CambioEnergia(float act, float max);
    public CambioEnergia ActEnergia;

    public CambioEnergia ActVida;


    public Rigidbody2D rb;

    public GameObject popText;


    public virtual void Awake()
    {
        if (usarVidaSimple)
        {
            vidaAct = partesDebiles.Length;
            vidaMax = partesDebiles.Length;
        }
    }


    public void Start()
    {
        for (int i = 0; i < partesDebiles.Length; i++)
        {
            partesDebiles[i].dañoRecibido += RecibirDaño;
        }
    }

    

    public virtual void RecibirDaño(float dañoRecibido, Collision2D other)
    {
        if (dead || invulnerable) return;

        if (usarVidaSimple)
        {
            vidaAct--;
        }
        else
        {
            vidaAct -= dañoRecibido;
            CrearTextoPop(other.contacts[0].point, dañoRecibido);
        }

        if (ActVida != null) ActVida(vidaAct, vidaMax);
        if (vidaAct <= 0) Muerto();

    }

    public virtual GameObject CrearTextoPop(Vector3 pos, float dañoRecibido)
    {
        GameObject clone = Instantiate<Object>(popText, null) as GameObject;
        clone.transform.rotation = Quaternion.Euler(0, 0, 0);
        clone.transform.position = pos;
        clone.GetComponent<PopText>().Activar(dañoRecibido.ToString("0"));
        return clone;

    }

  public  IEnumerator TiempoInvulnerable(float x)
    {
        yield return new WaitForSeconds(x);
        invulnerable = false;
    }

    public void KnockBack(Collision2D other)
    {
        if (knockBack) rb.AddForce((Vector2)transform.position - (other.contacts[0].point).normalized * -knockBackForce, ForceMode2D.Impulse);
    }

    public void Update()
    {
        RecargaEnergia();
    }

    public void RecargaEnergia()
    {
        if (energiaAct == 100)      return;
        else if (energiaAct > 100)  energiaAct = 100;
        else if(energiaAct < 100)   energiaAct += Time.deltaTime * velRecargaEnergia;        
        if (ActEnergia != null) ActEnergia(energiaAct, energiaMax);
    }


    public virtual void Muerto()
    {
        dead = true;
        StopAllCoroutines();
        //print(gameObject.name + "Unidad Muerta");
        StartCoroutine(AnimacionMuerte());
    }


    public IEnumerator AnimacionMuerte()
    {
        float t = 0;
        Color cActual = spriteEnergia.color;
        while(t < 1)
        {
            t += Time.deltaTime * 0.5f;

            spriteEnergia.color = Color.Lerp(cActual, Color.black, t);
            yield return null;
        }


    }


    public abstract void FxImpacto();

}
