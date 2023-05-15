using UnityEngine.UI;
using System.IO;
using UnityEngine;

public class NombresBotones : MonoBehaviour
{
    private const string NombreArchivo = "/estudiantes.json";
    public int Estnum;
    private DatoEstudiante est;

    void Update()
    {
        Button miboton = GetComponent<Button>();

        string path = Application.streamingAssetsPath + NombreArchivo;
        string estudiantesString = File.ReadAllText(path);
        DatosEstudiantes datosEstudiantes = JsonUtility.FromJson<DatosEstudiantes>(estudiantesString);

        Text textoboton = miboton.GetComponentInChildren<Text>();

        est = datosEstudiantes.estudiantes[Estnum];


        textoboton.text = est.Nombre;

    }
}
