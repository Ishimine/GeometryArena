using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Misil_Tipo_A : Unidad
{
    public ForceMode2D TipoDeFuerza;
    Rigidbody2D rb;

   public bool activado = false;
    public Transform target;
    public Transform puntoPropulsion;
    public float smooth = .5f;
    public float velocidadRotacion = 10f;
    public float fPropulsion = 300f;
    float vel;


    new void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = FindObjectOfType<PlayerSwipeMovement>().transform;
    }

    new void Update()
    {
        if (!activado || target == null)
            return;

        MirarTarget();
        Propulsion();
    }


    void MirarTarget()
    {
        Vector2 dir = target.transform.position - transform.position;
        float anguloObjetivo = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;                                                    //Angulo de la rotacion
                                                                                                                             //float anguloActual = 

       transform.rotation = Quaternion.AngleAxis(Mathf.SmoothDampAngle(transform.rotation.eulerAngles.z, anguloObjetivo,ref vel, smooth, velocidadRotacion, Time.deltaTime),Vector3.forward);


       /* Quaternion rotObjetivo = Quaternion.AngleAxis(anguloObjetivo, Vector3.forward);
        transform.rotation = rotObjetivo;*/


        //transform.rotation = Quaternion.AngleAxis(Mathf.SmoothDampAngle(transform.rotation.z,anguloObjetivo,ref vel, smooth, velocidadRotacion, Time.deltaTime),Vector3.forward);

        // transform.rotation = Quaternion.AngleAxis(velocidadRotacion * Mathf.Sign(Quaternion.Angle(transform.rotation,rotObjetivo)), Vector3.forward);

        //float difAngle = Quaternion.Angle(transform.rotation, rotObjetivo)


    }

    void Propulsion()
    {
        //rb.AddForceAtPosition(puntoPropulsion.up * fPropulsion, puntoPropulsion.position, ForceMode2D.Force);
        rb.AddRelativeForce(Vector3.right * fPropulsion, TipoDeFuerza);
    }

    public override void FxImpacto()
    {
        Debug.Log("FxImpacto: " + gameObject.name);
        Destroy(gameObject);
    }
}
