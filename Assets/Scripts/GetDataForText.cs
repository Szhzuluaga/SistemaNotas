using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GetDataForText : MonoBehaviour
{
    private const string NombreArchivo = "/estudiantes.json";
    public int Estnum;
    private DatoEstudiante est;
    public DatosEstudiantes estudiantes;
    private Text text;

    public void Start()
    {
        string path = Application.streamingAssetsPath + NombreArchivo;
        string estudiantesString = File.ReadAllText(path);
        DatosEstudiantes datosEstudiantes = JsonUtility.FromJson<DatosEstudiantes>(estudiantesString);
        est = datosEstudiantes.estudiantes[Estnum];
        text = GetComponent<Text>();
        text.text = "Nombre: " + est.Nombre + "\n"
            + "Apellido: " + est.Apellido + "\n"
            + "Codigo: " + est.Codigo + "\n"
            + "Correo: " + est.Correo + "\n"
            + "Nota: " + est.Nota;

    }
}
