using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopText : MonoBehaviour {

    public Rigidbody2D rb;
    public TextMesh txt;


    public void Activar(string t)
    {
        rb.AddForce(Vector2.up * 2, ForceMode2D.Impulse);
        gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        txt.text = t;
        Destroy(gameObject, 2);
    }
}
