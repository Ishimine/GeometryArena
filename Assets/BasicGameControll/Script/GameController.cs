using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {


    public Slider_UI slider;
    public CameraFollow camFollow;
    public static GameController instance;
    public CanvasControll canvas;

    public static bool enPausa = false;
    public static bool enJuego = false;

    public delegate void Trigger();
    public Trigger SesionJuegoIniciada;
    public Trigger SesionJuegoTerminada;

    public Trigger JuegoPausado;

    public GameObject player;


    private void Awake()
    {
        if(instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
            // SceneManager.sceneLoaded += CargaTerminada;
            SelectorDeNiveles.instance.NivelCargado += CargaTerminada;
            if(SceneManager.GetActiveScene().buildIndex >0)
            {
                IniciarSesionDeJuego();
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void CargaTerminada(Scene n, LoadSceneMode l)
    {
        CargaTerminada();
    }

    void CargaTerminada()
    {
        if(SceneManager.GetActiveScene().buildIndex != 0)
        {            
            IniciarSesionDeJuego();
        }
        else
        {
            EnMenu();
        }
    }

    public void EnMenu()
    {
        enJuego = false;
        CanvasControll.instance.MenuPrincipal();
    }
    /// <summary>
    /// Ejemplo: se activa al iniciar la carrera
    /// </summary>
    public void IniciarSesionDeJuego()
    {        
        GameObject clone = Instantiate<Object>(player, null) as GameObject;
        camFollow.SetTarget(clone.GetComponent<PlayerSwipeMovement>(), clone.GetComponent<Collider2D>(), clone.GetComponent<Rigidbody2D>());
        enJuego = true;
        canvas.EnJuego();
        clone.GetComponent<PlayerSwipeMovement>().ActVida += slider.Actualizar;
        if (SesionJuegoIniciada != null) SesionJuegoIniciada();
        enPausa = false;
        SpawnearEnemigos();
    }


    public void SpawnearEnemigos()
    {
        Compuerta[] compuertas = FindObjectsOfType<Compuerta>();
        foreach (Compuerta c in compuertas)
        {
            c.CrearUnidad();
        }
    }


    /// <summary>
    /// Se activa al terminar la sesion de juego, ejemplo, se llega a la meta de la pista, o muere un personaje en un duelo
    /// </summary>
    public void TerminarSesionDeJuego()
    {
        if (SesionJuegoTerminada != null) SesionJuegoTerminada();
        StartCoroutine(TiempoPostSesionJuego());
    }

    IEnumerator TiempoPostSesionJuego()
    {
        yield return new WaitForSeconds(2);
        CanvasControll.instance.GameOver();
    }

    public void Pausa()
    {
        if(enPausa)
        {
            Time.timeScale = 1;
            CanvasControll.instance.EnJuego();
        }
        else
        {
            StopAllCoroutines();
            Time.timeScale = 0;
            CanvasControll.instance.Pausa();
        }
        if (JuegoPausado != null) JuegoPausado();
        enPausa = !enPausa;
    }

    private void Update()
    {
        
    }

   public void GameOver()
    {
        TerminarSesionDeJuego();
    }
  
}
