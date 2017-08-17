using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(UI_MenuNiveles))]
public class UI_MenuNivelesEditor : Editor
{

    public override void OnInspectorGUI()
    {
        UI_MenuNiveles myTarget = (UI_MenuNiveles)target;
        DrawDefaultInspector();

        if (GUILayout.Button("CrearBotones"))
        {
            myTarget.ActualizarBotones();
        }
    }
}
