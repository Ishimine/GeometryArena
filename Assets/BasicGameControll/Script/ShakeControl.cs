using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeControl : MonoBehaviour {

    public static ShakeControl instance;

    public enum FuerzaShake {Debil, Medio, Fuerte}
    public FuerzaShake fuerza;
    public bool debugShake = false;

	public CameraShake.Properties debil;
	public CameraShake.Properties medio;
	public CameraShake.Properties fuerte;
    public CameraShake camShake;

    private void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

   public void ActivarShakeNormal(FuerzaShake potencia)
    {
        switch (potencia)
        {
            case FuerzaShake.Debil:
                camShake.StartShake(debil);
                break;
            case FuerzaShake.Medio:
                camShake.StartShake(medio);
                break;
            case FuerzaShake.Fuerte:
                camShake.StartShake(fuerte);
                break;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ActivarShakeNormal(fuerza);
        }
    }

}
