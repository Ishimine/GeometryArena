using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compuerta : MonoBehaviour {

    
    public Animator anim;
    public GameObject unidad;
    public Unidad ultimaUnidadCreada;
    public GameObject contenedorUnidad;
    Vector2 inicial = new Vector2(0.6f, 0.6f);

    public void SetCapa()
    {

    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            CrearUnidad();
        }
    }

    public void CrearUnidad()
    {
        GameObject clone = Instantiate(unidad, contenedorUnidad.transform);
        contenedorUnidad.transform.localScale = inicial;
        ultimaUnidadCreada = clone.GetComponent<Unidad>();
        AbrirPuerta();
    }


    public void SubirUnidadCreada()
    {
        StartCoroutine(SubirUnidad());
    }

    IEnumerator SubirUnidad()
    {
        //ultimaUnidadCreada.SetCapa();
       
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / 2;
            contenedorUnidad.transform.localScale = Vector2.Lerp(inicial, Vector2.one, t);
            yield return null;
        }
        unidad.transform.SetParent(null);
        ActivarUnidad();
    }

    IEnumerator EsperarYActivar(float f, Unidad u)
    {
        yield return new WaitForSeconds(f);
    }


    public void ActivarUnidad()
    {
        //CerrarPuerta();
        ultimaUnidadCreada.Activar();
    }

    public void AbrirPuerta()
    {
        anim.SetBool("Abierto", true);
    }

    public void CerrarPuerta()
    {
        anim.SetBool("Abierto",false);
    }
}
