using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parpadeo : MonoBehaviour {

    float actual;
    public float tiempo = 1f;

    bool prendido = false;
   

    SpriteRenderer render;

    private void Awake()
    {
        render = GetComponent<SpriteRenderer>();
    }

    void Prender()
    {
        render.enabled = false;
    }
    void Apagar()
    {
        render.enabled = true;
    }
    // Update is called once per frame
    void Update ()
    {
        actual += Time.deltaTime;
        if(actual > tiempo)
        {
            actual = 0;
            prendido = !prendido;
            if (prendido)
                Prender();
            else
                Apagar();
        }
	}
}
