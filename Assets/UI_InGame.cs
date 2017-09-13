using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_InGame : MonoBehaviour {



    public static UI_InGame instance;
    public GameObject uiNormal;
    public GameObject uiSurvival;


    private void Awake()
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

    public void SetActive_UI_Normal(bool x)
    {
        uiNormal.SetActive(x);
    }

    public void SetActive_UI_Survival(bool x)
    {
        uiSurvival.SetActive(x);
    }
}
