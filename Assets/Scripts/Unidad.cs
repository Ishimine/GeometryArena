using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unidad : MonoBehaviour
{
    public bool activado;
    public Vector2 posObj;
    public AudioSource sonido;
    public Transform target;
    public bool dead = false;
    public bool invulnerable = false;

    public bool knockBack = true;
    public float knockBackForce = 10;

    public bool usarVidaSimple = true;

    public float velRecargaEnergia;
    public int vidaAct;
    public int vidaMax = 1;

    public float energiaAct;
    public float energiaMax = 100;

    public List<Parte_PuntoDebil> puntosDebiles = new List<Parte_PuntoDebil>();

    public SpriteRenderer spriteEnergia;

    public delegate void CambioEnergia(float act, float max);
    public CambioEnergia ActEnergia;

    public CambioEnergia ActVida;


    public Rigidbody2D rb;

    public GameObject popText;


    public Vector3 dir;
    public float velMov = 20;
    public float smooth = .5f;
    public float velocidadRotacion = 10f;
    public float fPropulsion = 300f;
    float vel;


    Color colorOrig;
    Color colormuerte;


    public void AgregarPuntoDebil(Parte_PuntoDebil p)
    {
        puntosDebiles.Add(p);
    }

    public void QuitarPuntoDebil(Parte_PuntoDebil p)
    {
        puntosDebiles.Remove(p);
    }

    public void MirarTarget()
    {
        Vector2 dir = target.transform.position - transform.position;
        float anguloObjetivo = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;                                                    //Angulo de la rotacion
                                                                                                                             //float anguloActual = 
        transform.rotation = Quaternion.AngleAxis(Mathf.SmoothDampAngle(transform.rotation.eulerAngles.z, anguloObjetivo, ref vel, smooth, velocidadRotacion, Time.deltaTime), Vector3.forward);
    }

    public virtual void Awake()
    {
        InicializarUnidad();
    }

    public void InicializarUnidad()
    {

        /*if (spriteEnergia != null)
        {
            colorOrig = spriteEnergia.color;
            colormuerte = Color.red;
        }*/
        rb = GetComponent<Rigidbody2D>();
        sonido = GetComponent<AudioSource>();
    }

    public virtual void Start()
    {
        CalcularVida();
    }

    public virtual void CalcularVida()
    {
        vidaAct = puntosDebiles.Count;
        vidaMax = vidaAct;
    }
    
    public void SetPosObjetivo(Vector3 posObj)
    {
        this.posObj = posObj;
    }

    public virtual void RecibirDaño()
    {
        if (dead || invulnerable) return;        
        vidaAct--;

       // if (spriteEnergia != null) spriteEnergia.color = Color.Lerp(colorOrig, colormuerte, vidaAct / vidaMax);


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

    public virtual void FixedUpdate()
    {
        if (!activado) return;

        posObj = target.position;
        CalcularDir();
        MovPorRb();
    }

    public virtual void CalcularDir()
    {
       dir =  (posObj - (Vector2)transform.position).normalized;
    }

    public void MovPorRb()
    {
        rb.AddForce(dir * velMov, ForceMode2D.Force);
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
        activado = false;
        StopAllCoroutines();
        //print(gameObject.name + "Unidad Muerta");
        StartCoroutine(AnimacionMuerte());
        Destroy(this.gameObject, 5f);

    }

    public virtual void OnDestroy()
    {
        SistemaAlerta.QuitarEnemigo(this.gameObject);
    }


    public IEnumerator AnimacionMuerte()
    {
        if (spriteEnergia == null)
            yield break;
        float t = 0;
        Color cActual = spriteEnergia.color;
        while(t < 1)
        {
            t += Time.deltaTime * 0.5f;
            spriteEnergia.color = Color.Lerp(cActual, Color.black, t);
            yield return null;
        }
    }

    public IEnumerator EsperaActivacion()
    {
        yield return new WaitForSeconds(1);
        if (target == null) BuscarTarget();
    }


    public virtual void Activar()
    {
        activado = true;
        BuscarTarget();
        SistemaAlerta.AgregarEnemigo(this.gameObject);
    }

    

    


    public void BuscarTarget()
    {
        target = FindObjectOfType<PlayerSwipeMovement>().transform;
    }

    public abstract void FxImpacto();
    public abstract void FxImpactoRealizado();
    public abstract void FxImpactoRealizado(Vector3 impactPos);

}
