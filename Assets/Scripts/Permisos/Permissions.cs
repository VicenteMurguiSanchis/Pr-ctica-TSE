using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class Permissions : MonoBehaviour
{
    //El texto de la UI que indicará si los permisos han sido solicitados
    [SerializeField] private TextMeshProUGUI permissionFineLocation;

    //Variable que indica si la aplicación está solicitando los permisos o no
    private bool solicitando = false; 

    // Start is called before the first frame update
    void Start()
    {
        //La aplicación inicia con los permisos no concedidos
        UpdateText("Rechazado", Color.red);

        PedirPermisos();
    }

    // Update is called once per frame
    void Update()
    {
        //En caso de que el permiso haya sido otorgado, se actualiza el texto a "Otorgado"
        if (Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            UpdateText("Otorgado", Color.green);
            solicitando = false;
        }

        //En caso contrario se activa la corrutina que permitirá solicitar un nuevo permiso cada 2 segundos
        else
        {
            StartCoroutine(EsperaSolicitudPermiso());
        }
    }

    //Función que pide los permisos de ubicación al usuario al iniciar la aplicación
    void PedirPermisos()
    {
        //En caso de que no estén concedidos, se solicita al usuario que los conceda
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);

            UpdateText("Rechazado", Color.red);

            solicitando = true;
        }

        //En caso de que estén concedidos, el UI de los permisos se actualiza a "Otorgado"
        else
        {
            UpdateText("Otorgado", Color.green);
        }
    }

    //La siguiente corrutina permite al programa solicitar los permisos de ubicación cada 2 segundos después de que el usuario responda a la solicitud del sistema
    IEnumerator EsperaSolicitudPermiso()
    {
        solicitando = true;
        yield return new WaitForSeconds(2);
        Permission.RequestUserPermission (Permission.FineLocation);
        solicitando = false;
    }

    //El texto de la UI se actualiza en función de la respuesta a la solicitud, alterando el contenido del texto y el color
    void UpdateText(string text, Color color)
    {
        permissionFineLocation.text = text;
        permissionFineLocation.color = color;
    }
}
