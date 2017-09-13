using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonSurvival : MonoBehaviour {

	
    public void ModoSurvival()
    {
        GameController.SetModo(GameController.modoDeJuego.Survival);
        SelectorDeNiveles.CargarSurvival();
    }
}
