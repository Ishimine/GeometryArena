using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchControl : MonoBehaviour {


    public float potenciaMaxima = 10;

    public bool usarReferencia = true;
    public GameObject swipeRef;

    [SerializeField] bool usarMouse = false;

    Vector3 tInicial;
    Vector3 tActual;
    Vector3 tFinal;

    public delegate void Trigger(Vector2 dir, float potencia);
    public Trigger DireccionPotencia;
    public Trigger DireccionEstacionaria;


    public delegate void Act();
    public Act touchEstacionario;
    public Act touchOut;



    private void Awake()
    {
        if (!Application.isEditor)
        {
            usarMouse = false;
        }
        else
        {
            usarMouse = true;
        }
    }


    private void Update()
    {
        if (!GameController.enJuego)
            return;

        if (Input.touchCount > 0)
        {
            Touch(Input.GetTouch(0));
        }

        if (usarMouse)
        {
            Mouse();
        }

        
    }

    void Mouse()
    {
        Touch t = new Touch();
        t.position = Input.mousePosition;
        if (Input.GetMouseButtonDown(0))
        {
            t.phase = TouchPhase.Began;
            Touch(t);
        }
        else if (Input.GetMouseButton(0))
        {
            t.phase = TouchPhase.Moved;
            Touch(t);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            t.phase = TouchPhase.Ended;
            Touch(t);
        }


    }


    void Touch(Touch t)
    {
        if (t.phase == TouchPhase.Began)
        {
            if(usarReferencia) PosicionarRefSwipe(Camera.main.ScreenToWorldPoint(t.position) + Vector3.forward * 10);
            TouchIn(t);
        }
        else if (t.phase == TouchPhase.Moved || t.phase == TouchPhase.Stationary)
        {
            if (usarReferencia) ActualizarRefSwipe(t.position);
            TouchMove(t);
            if (touchEstacionario != null) touchEstacionario();
        }
        else if (t.phase == TouchPhase.Ended)
        {
            if (usarReferencia) DesactivarRefSwipe();
            TouchOut(t);
            if (touchOut != null) touchOut();
        }
    }


    public void TouchIn(Touch t)
    {
        tInicial = t.position;
    }
    

    void TouchMove(Touch t)
    {
        tActual = t.position;



        if (DireccionEstacionaria != null)
        {
            tFinal = t.position;
            Vector2 dir = tFinal - tInicial;
            float dist = Vector2.Distance(tFinal, tInicial);
            if (dist > potenciaMaxima) dist = potenciaMaxima;

            DireccionEstacionaria(dir, dist);
        }

    }

    void TouchOut(Touch t)
    {
        tFinal = t.position;
        Vector2 dir = tFinal - tInicial;
        float dist = Vector2.Distance(tFinal, tInicial);

        if (dist > potenciaMaxima) dist = potenciaMaxima;


        if (DireccionPotencia != null) DireccionPotencia(dir,dist);


    }

    void PosicionarRefSwipe(Vector3 pos)
    {
        swipeRef.transform.localScale = new Vector3(0, 0, 0);
        swipeRef.transform.position = pos;
        swipeRef.SetActive(true);
    }

    void ActualizarRefSwipe(Vector3 pos)
    {

        swipeRef.transform.position = Camera.main.ScreenToWorldPoint(tInicial) + Vector3.forward*10;

        pos = Camera.main.ScreenToWorldPoint(pos);
        Vector2 dir = pos - swipeRef.transform.position;
        float dist = Vector2.Distance(pos, swipeRef.transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;                                                    //Angulo de la rotacion
        swipeRef.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        swipeRef.transform.localScale = new Vector3(dist, 1, 1);
    }

    void DesactivarRefSwipe()
    {
        swipeRef.SetActive(false);
    }

}
