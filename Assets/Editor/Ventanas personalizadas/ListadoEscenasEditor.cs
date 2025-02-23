using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;


public class ListadoEscenasEditor : EditorWindow
{
    [MenuItem("Window/Listado de Escenas")]

    //Mostrar las ventana
    public static void ShowWindow()
    {
        GetWindow<ListadoEscenasEditor>("Listado de Escenas");
    }

    //Dibujado de la ventana
    private void OnGUI()
    {
        //Se añade un título a la ventana
        GUILayout.Label("Escenas en Build Settings", EditorStyles.boldLabel);

        EditorBuildSettingsScene[] listado_escenas = EditorBuildSettings.scenes;

        string actual_scene_name = SceneManager.GetActiveScene().name;

        //Se muestra el listado de escenas del Build Setting
        UpdateListadoEscenas(listado_escenas, actual_scene_name);

        GUILayout.Space(20);

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        //El siguiente botón se encarga de recargar la escena actual
        if (GUILayout.Button("Recargar escena actual", GUILayout.Width(150)))
        {
            EditorSceneManager.OpenScene(SceneManager.GetActiveScene().path);
            Debug.Log("Escena " + SceneManager.GetActiveScene().name + " cargada");
        }

        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        GUILayout.Space(20);

        //Se muestra un cuadro en el cual se podrán arrastrar escenas para ser añadidas al listado de Build Setting 
        Rect campoDrop = GUILayoutUtility.GetRect(20, 50, 10, 60);
        GUI.Box(campoDrop, "Arrastra sobre este campo la escena que quieras añadir a Build Settings");
        

        Event e = Event.current;

        //Los eventos que han de tenerse en cuenta son los de Arrastrar y Soltar elementos
        if (e.type == EventType.DragUpdated || e.type == EventType.DragPerform) 
        {
            if(campoDrop.Contains(e.mousePosition))
            {
                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

                //En caso de que se suelte el elemento, se ejecuta la siguiente lógica
                if(e.type == EventType.DragPerform)
                {
                    Debug.Log("Escena soltada en el cuadro Drop");
                    DragAndDrop.AcceptDrag();

                    //En caso de que se estén arrastrando varios objetos, solo se agregarán los que sean del tipo scene
                    foreach (Object obj in DragAndDrop.objectReferences)
                    {
                        if(obj is SceneAsset)
                        {
                            AgregarEscena(AssetDatabase.GetAssetPath(obj));
                        }
                    }

                    Event.current.Use();
                }
            }
        }
    }

    //Actualiza el listado de escenas de Build Setting, teniendo en cuenta cual es la escena cargada actualmente
    private void UpdateListadoEscenas(EditorBuildSettingsScene[] listado_escenas, string actual_scene_name)
    {
        //Si en el Build Settings hay escenas, las muestra en la ventana
        if (listado_escenas.Length > 0)
        {
            foreach (EditorBuildSettingsScene esc in listado_escenas)
            {
                string esc_name = System.IO.Path.GetFileNameWithoutExtension(esc.path);

                GUILayout.BeginHorizontal();

                GUILayout.Label(esc_name);

                GUILayout.FlexibleSpace();

                //Si la escena añadida no es la actual, a su derecha se mostrará un botón que permitirá cargar dicha escena
                if (esc_name != actual_scene_name)
                {
                    if (GUILayout.Button("Cargar Escena", GUILayout.Width(100)))
                    {
                        Debug.Log("Escena " + esc_name + " cargada");
                        EditorSceneManager.OpenScene(esc.path);
                    }
                }

                //En caso contrario, se mostrará un Label que indicará que dicha escena es la escena actual
                else
                {
                    GUILayout.Label("Escena actual", EditorStyles.boldLabel);
                }

                GUILayout.EndHorizontal();
            }
        }
    }

    //La función se encarga de agregar una escena al Build Setting, comprobando si ésta ya se encuentra en el listado
    private void AgregarEscena(string pathScene)
    {
        //Sacamos el listado de escenas en una lista para poder añadir la nueva escena en caso de ser necesario
        List<EditorBuildSettingsScene> listado_escenas = new List<EditorBuildSettingsScene>(EditorBuildSettings.scenes);

        //Se comprueba si la escena nueva se encuentra en el Build Setting
        foreach (EditorBuildSettingsScene esc in listado_escenas)
        {
            //Si se encuentra en el Build Setting, la función finaliza
            if(esc.path == pathScene)
            {
                Debug.Log("La escena ya se encuentra en el Build Setting");
                return;
            }
        }

        //En caso de que la escena nueva no esté en el Build Setting, se añade al listado de escena y este sustituye al array de escenas del Build Setting
        listado_escenas.Add(new EditorBuildSettingsScene(pathScene, true));
        EditorBuildSettings.scenes = listado_escenas.ToArray();

        Debug.Log("Nueva escena cargada en el listado de escenas: " + pathScene);
    }
}
