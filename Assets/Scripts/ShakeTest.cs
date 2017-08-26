using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeTest : MonoBehaviour {

	public CameraShake.Properties debil;
	public CameraShake.Properties fuerte;

    CameraShake camShake;

    private void Awake()
    {
        camShake = FindObjectOfType<CameraShake>();
    }
    

    public void Fuerte()
    {
        camShake.StartShake(fuerte);
    }

    public void Debil()
    {
        camShake.StartShake(debil);
    }



}
