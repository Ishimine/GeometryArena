using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slider_UI : MonoBehaviour {

    public static Slider_UI instance;

    Slider slider;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            slider = gameObject.GetComponent<Slider>();
            SetTarget();
            //SelectorDeNiveles.instance.NivelCargado += SetTarget;
        }
        else
        {
            Destroy(this);
        }
    }


   
    
    void SetTarget()
    {
        //PlayerSwipeMovement.instance.ActVida += Actualizar;
        if (FindObjectOfType<PlayerSwipeMovement>()) FindObjectOfType<PlayerSwipeMovement>().ActVida += Actualizar;
    }

    public void Actualizar(float act, float max)
    {
       if(slider != null) slider.value = act / max;
    }

    private void OnDestroy()
    {
        if(FindObjectOfType<PlayerSwipeMovement>())        FindObjectOfType<PlayerSwipeMovement>().ActVida -= Actualizar;
        // PlayerSwipeMovement.instance.ActVida -= Actualizar;
    }
}
