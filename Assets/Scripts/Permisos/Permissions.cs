using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class Permissions : MonoBehaviour
{
    //Texto de la interfaz que indica si los permisos de ubicación han sido otorgados o rechazados
    [SerializeField] private TextMeshProUGUI permissionFineLocation;

    //Variable que sirve para evitar múltiples solicitudes simultáneas e indicar cuando la aplicación está solicitando los permisos
    private bool solicitando = false; 

    void Start()
    {
        //La aplicación inicia con la interfaz indicando que permisos no concedidos
        UpdateText("Rechazado", Color.red);
        PedirPermisos();
    }

    void Update()
    {
        //Comprueba si los permisos de ubicación han sido concedidos
        if (Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            UpdateText("Otorgado", Color.green);
            solicitando = false;
        }
        //En caso contrario, solicita de nuevo la corrutina si no se está solicitando los permisos en ese momento
        else if (!solicitando) 
        {
            StartCoroutine(EsperaSolicitudPermiso());
        }
    }

    //Función que pide los permisos de ubicación al usuario al iniciar la aplicación
    void PedirPermisos()
    {
        //Comprueba si los permisos han sido concedidos
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
            UpdateText("Rechazado", Color.red);
        }
        else
        {
            UpdateText("Otorgado", Color.green);
        }
    }

    //La siguiente corrutina permite al programa solicitar los permisos de ubicación cada 2 segundos después de que el usuario responda a la solicitud del sistema
    IEnumerator EsperaSolicitudPermiso()
    {
        solicitando = true;
        // Espera 2 segundos antes de solicitar de nuevo los permisos de ubicación
        yield return new WaitForSeconds(2); 
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
        }
        solicitando = false;
    }

    //El texto de la UI se actualiza en función de la respuesta a la solicitud, alterando el contenido del texto y el color
    void UpdateText(string text, Color color)
    {
        permissionFineLocation.text = text;
        permissionFineLocation.color = color;
    }
}