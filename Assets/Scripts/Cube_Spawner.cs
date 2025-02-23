using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cube_Spawner : MonoBehaviour
{
    //Listado que almacena todas las instancias del prefab Cubo de la escena
    private List<GameObject> cubeList = new List<GameObject>();

    //Listado de los posibles materiales que puede tener el prefab Cubo
    [SerializeField] private List<Material> Colors = new List<Material>();

    //Texto de la interfaz que indica el número de instancias de Cubo que hay en la escena
    [SerializeField] private TextMeshProUGUI text_cube; 

    //GameObject que representa el objeto Cubo
    [SerializeField] private GameObject cube;

    //Elemento dentro de la jerarquía de la escena en el que se almacenarán todas las instancias de Cubo para que no saturen la jerarquía
    [SerializeField] private GameObject parentContainer;

    //Número de cubos que se crean al pulsar el botón "Crear_Cubo"
    [SerializeField] private int NumeroCubosCrear = 1;

    //Inicializa el contenido del texto que indica el número de cubos en la escena
    void Start()
    {
        text_cube.text = "Contador Cubos: " + cubeList.Count;
    }

    //Añade instancias del objeto Cubo en función de la variable NumeroCubosCrear al pulsar el botón "Crear_Cubo"
    public void SpawnCube()
    {
        for(int i = 0; i < NumeroCubosCrear; i++)
        {
            //Se crea la instancia del objeto mediante el objeto cube en una posición aleatoria delimitada por un rango
            GameObject c = Instantiate(cube, new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-3.5f, 5.5f), 0.0f), Quaternion.identity);
            //Se le añade un material aleatorio de entre los que hay en el listado Colors
            c.GetComponent<Renderer>().material = Colors[Random.Range(0,Colors.Count)];
            //Se añade el cubo como hijo del contenedor parentContainer
            c.transform.parent = parentContainer.transform;
            //Se añade el cubo a la lista de cubos
            cubeList.Add(c);
            Debug.Log("Nuevo cubo añadido");
        }

        UpdateText();
    }

    //Elimina todas las instancias de Cubo que hay en la escena
    public void ClearCubes()
    {
        //Recorre el listado de Cubos eliminando todos los elementos almacenados en este
        foreach(GameObject c in cubeList)
        {
            if(c != null)
                DestroyImmediate(c);
        }

        cubeList.Clear();
        UpdateText();

        Debug.Log("Cubos Eliminados");
    }

    //Actualiza el texto de la interfaz que indica el número de cubos que hay en la escena
    public void UpdateText()
    {
        text_cube.text = "Contador Cubos: " + cubeList.Count;
        Debug.Log("Numero de cubos actuales: " + cubeList.Count);
    }
}
