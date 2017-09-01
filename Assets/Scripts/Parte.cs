using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parte : MonoBehaviour
{
    public AudioSource sonido;

    public enum tipoParte {Ofensivo, Destructible, Indestructible, PuntoDebil}
    public tipoParte tipo;


    public SpriteRenderer render;
    public Color colorOriginal;
    [SerializeField] public Unidad unidad;
    public Collider2D col;   



    private void Awake()
    {
        InicializarParte();
    }

    public void ReproducirSonido(AudioClip fx)
    {
        if (fx == null) return;
        sonido.clip = fx;
        sonido.Play();
    }

    public virtual void InicializarParte()
    {
        SpriteRenderer rend = GetComponent<SpriteRenderer>();
        if (rend != null)
        {
            render = rend;
            colorOriginal = render.color;
        }     col = GetComponent<Collider2D>();
    }


    private void Reset()
    {
        if (col == null) InicializarParte();
        if (unidad == null) BuscarUnidadEnJerarquia();
        if (unidad == null)
            Debug.Log("ALERTA: componente unidad INEXISTENTE en la jerarquia");
    }

    public virtual void BuscarUnidadEnJerarquia()
    {
        if (unidad != null)
            return;
        Transform x = transform;
        unidad = GetComponent<Unidad>();

        while ((unidad == null) && (x != null))
        {
            x = x.parent;
            if(x != null)
                unidad = x.GetComponent<Unidad>();
        }
        if (unidad == null)
            DestroyImmediate(this);
    }


    public virtual void OnCollisionEnter2D(Collision2D other)
    {
        if (unidad.dead || unidad.invulnerable) return;
        unidad.FxImpactoRealizado();
    }
    

}
