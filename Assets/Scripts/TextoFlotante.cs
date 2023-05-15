using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TextoFlotante : MonoBehaviour
{
    private const string NombreArchivo = "/estudiantes.json";
    public int Est;
    public DatosEstudiantes estudiantes;
    private List<DatoEstudiante> estudiantesList;
    private DatoEstudiante est;
    
    void Start()
    {
        this.gameObject.SetActive(true);
        string path = Application.streamingAssetsPath + NombreArchivo;
        string estudiantesString = File.ReadAllText(path);
        DatosEstudiantes datosEstudiantes = JsonUtility.FromJson<DatosEstudiantes>(estudiantesString);
        est = datosEstudiantes.estudiantes[Est];
        TextMesh textMesh = this.GetComponent<TextMesh>();
        textMesh.text = est.Nombre;
    }

    
}
