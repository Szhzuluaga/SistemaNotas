using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class ObtenerError : MonoBehaviour
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
        if(est.Nota < 3.0f)
        {
            text.text = "El estudiante: " + est.Nombre + " está mal ubicado, su nota es inferior a 3.0";
        }
        else if(est.Nota >= 3.0)
        {
            text.text = "El estudiante " + est.Nombre + " está mal ubicado, su nota es superior a 3.0";
        }
        this.gameObject.SetActive(false);
    }
}
