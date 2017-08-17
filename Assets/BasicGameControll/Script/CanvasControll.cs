using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasControll : MonoBehaviour
{

    public Animator anim;
    public static CanvasControll instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void EnJuego()
    {
      //  Debug.Log("EnJuego");
        anim.SetTrigger("InGame");
    }

    public void MenuNiveles()
    {
      //  Debug.Log("MenuNiveles");
        anim.SetTrigger("MenuNiveles");
    }
    
    public void MenuPrincipal()
    {
    //    Debug.Log("MenuPrincipal");
        anim.SetTrigger("MenuPrincipal");
    }

    public void Pausa()
    {
        anim.SetTrigger("Pausa");
       // Debug.Log("Pausa");

    }

    public void CargandoEscena()
    {
        anim.SetTrigger("CargandoEscena");
    }

    public void GameOver()
    {
        anim.SetTrigger("GameOver");
    }
}
