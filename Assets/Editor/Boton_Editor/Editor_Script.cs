using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Cube_Spawner))]
public class Editor_Script : Editor
{
    //Variable que indica cuantos cubos se crean al pulsar el botón Crear_Cubo
    int cantidadCubos = 1;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        //Las modificacions del Inspector serán accesibles en el componente Cube_Spawner
        Cube_Spawner cube_Spawner = (Cube_Spawner)target;

        //Se agrega el slider que indicará cuantos cubos se crean con la pulsación
        cantidadCubos = EditorGUILayout.IntSlider("Numero de cubos a crear", cantidadCubos, 1, 100);

        //El botón Crear_Cubo se encargará de añadir una instancia Cubo en la escena
        if(GUILayout.Button("Crear Cubo"))
        {
            Debug.Log("Botón Crear_Cubo pulsado");
            cube_Spawner.SpawnCube(cantidadCubos);
        }

        //El botón Eliminar_Cubo se encargará de eliminar todas las instancias Cubo en la escena
        if (GUILayout.Button("Eliminar Cubos"))
        {
            Debug.Log("Botón Eliminar_Cubo pulsado");
            cube_Spawner.ClearCubes();
        }
    }
}
