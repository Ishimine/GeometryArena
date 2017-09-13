using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectorDeNiveles : MonoBehaviour{

    public static SelectorDeNiveles instance;
    public delegate void Trigger();
    public Trigger NivelCargado;

    public delegate void Actualizacion(float i);
    public Actualizacion porcentajeDeCarga;

    public void Awake()
    {
        if (instance == null)
            instance = this;
    }


    public static void CargarSurvival()
    {
        SceneManager.LoadScene("Survival");
    }

    public static void CargarNivel(int n)
    {
        Time.timeScale = 1;
        if (n < SceneManager.sceneCountInBuildSettings)
        {
        instance.CargarAsincrono(n);
        }
        else
        {
        instance.CargarAsincrono(0);
        }

    }

    public static void Restart()
    {
        CargarNivel(SceneManager.GetActiveScene().buildIndex);
        CanvasControll.instance.EnJuego();
    }

    public static void CargarMenu()
    {
        CargarNivel(0);
        CanvasControll.instance.MenuPrincipal();
    }

    public static bool CargarNivelAnterior()
    {
        int i = SceneManager.GetActiveScene().buildIndex - 1;
        if(i < -1)
        {
            CargarNivel(i);
            CanvasControll.instance.EnJuego();
            return true;
        }
        else
        {
            Debug.Log("Intentando cargar nivel con ID menor a 0");
            return false;
        }
    }

    public static bool CargarNivelSiguiente()
    {
        int i = SceneManager.GetActiveScene().buildIndex + 1;
        if(i >= SceneManager.sceneCountInBuildSettings)
        {
            Debug.Log("Intentando cargar nivel con ID mayor a las IDs existentes");
            return false;
        }
        else
        {
            CanvasControll.instance.EnJuego();
            CargarNivel(i);
            return true;
        }
    }
    


    public void CargarAsincrono(int n)
    {
        StartCoroutine(CargarNivelAsincrono(n));
    }

    public static void ReiniciarNivel()
    {
        CargarNivel(SceneManager.GetActiveScene().buildIndex);
        CanvasControll.instance.EnJuego();
    }
        
    public IEnumerator CargarNivelAsincrono(int i)
    {
        CanvasControll.instance.CargandoEscena();
        AsyncOperation operation = SceneManager.LoadSceneAsync(i);



        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            if (porcentajeDeCarga != null) porcentajeDeCarga(progress);

            /* slider.value = progress;
             progresoTxt.text = (progress * 100).ToString("00.0") + "%";*/

            yield return null;
        }
        
        if (NivelCargado != null) NivelCargado();
    }


}