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

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fuerte();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            Debil();
        }


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
