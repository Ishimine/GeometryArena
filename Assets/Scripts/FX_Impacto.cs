using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FX_Impacto : MonoBehaviour {

    public SpriteRenderer render;
    public Animator anim;

    public bool destellar;
    public bool usarAnimator = false;
    public bool cambiarEscala = false;

    public bool UsarHijos = false;


    public float aumentoEscala = 1.1f;
    public Color cDestello = Color.white;
    [SerializeField]private Color original;
    public Color dañado = new Color(.7f,.5f,.3f,1);
    public Color cActual;
    public float velT = 0.2f;
    public float t = 0f;


    public Vector3 escalaOriginal;
    public Vector3 escalaMax;


    void Start ()
    {
        if(render == null) render = GetComponent<SpriteRenderer>();
        original = render.color;
    }

    private void OnValidate()
    {
        original = render.color;
    }

    private void Awake()
    {
        escalaOriginal = render.transform.localScale;
        escalaMax = render.transform.localScale * aumentoEscala;
    }

    
    public void Activar()
    {
        cActual = cDestello;
        StartCoroutine(DestelloFX(cDestello, original));
    }

    public void DestelloMuerte()
    {
        cActual = cDestello;
        StartCoroutine(DestelloFX(cDestello, Color.black));
    }

    IEnumerator DestelloFX(Color cDestello, Color cFinal)
    {
        t = 0;
        if (cambiarEscala) render.transform.localScale = escalaMax;
        do
        {
            cActual = Vector4.Lerp(cDestello, cFinal, t);
            if (cambiarEscala) render.transform.localScale = Vector4.Lerp(render.transform.localScale, escalaOriginal, t);
            if (destellar) render.color = cActual;
            t += velT * Time.deltaTime;
            yield return null;
        } while (t < 1);

        if (destellar) render.color = cFinal;
        if (cambiarEscala) render.transform.localScale = escalaOriginal;
        
    }
    
}
