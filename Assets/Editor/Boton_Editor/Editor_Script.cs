using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Cube_Spawner))]
public class Editor_Script : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        //Las modificacions del Inspector serán accesibles en el componente Cube_Spawner
        Cube_Spawner cube_Spawner = (Cube_Spawner)target;

        //El botón Crear_Cubo se encargará de añadir una instancia Cubo en la escena
        if(GUILayout.Button("Crear Cubo"))
        {
            Debug.Log("Botón Crear_Cubo pulsado");
            cube_Spawner.SpawnCube();
        }

        //El botón Eliminar_Cubo se encargará de eliminar todas las instancias Cubo en la escena
        if (GUILayout.Button("Eliminar Cubos"))
        {
            Debug.Log("Botón Eliminar_Cubo pulsado");
            cube_Spawner.ClearCubes();
        }
    }
}
