using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Globalization;

public class CargarDatosIniciales : MonoBehaviour
{
    private const string NombreArchivo = "/estudiantes.json";
    public InputField Name;
    public InputField Lastname;
    public InputField Code;
    public InputField Email;
    public InputField Grade;
    public int Estnum;
    private DatoEstudiante est;
    public DatosEstudiantes estudiantes;
   

    public void Start()
    {
        LoadJSON();
    }
    public void LoadJSON()
    {
        if (Estnum < 0 || Estnum > 5) {
            Estnum = 0;
        }

        string path = Application.streamingAssetsPath + NombreArchivo;
        string estudiantesString = File.ReadAllText(path);
        DatosEstudiantes datosEstudiantes = JsonUtility.FromJson<DatosEstudiantes>(estudiantesString);
        est = datosEstudiantes.estudiantes[Estnum];
        
         Name.text = est.Nombre;
         Lastname.text = est.Apellido;
         Code.text = est.Codigo;
         Email.text = est.Correo;
         Grade.text = est.Nota.ToString("N", CultureInfo.InvariantCulture);



    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Mouse0))
        {
            ActualizarDatos();
        }

    }

    public void ActualizarDatos()
    {
        string path = Application.streamingAssetsPath + NombreArchivo;
        string estudianteString = File.ReadAllText(path);
        Debug.Log(Estnum);
        DatosEstudiantes datosEstudiantes = JsonUtility.FromJson<DatosEstudiantes>(estudianteString);

        datosEstudiantes.estudiantes[Estnum].Nombre = Name.text;
        datosEstudiantes.estudiantes[Estnum].Apellido = Lastname.text;
        datosEstudiantes.estudiantes[Estnum].Codigo = Code.text;
        datosEstudiantes.estudiantes[Estnum].Correo = Email.text;
        datosEstudiantes.estudiantes[Estnum].Nota= float.Parse(Grade.text, CultureInfo.InvariantCulture);

        string updatedJsonDataString = JsonUtility.ToJson(datosEstudiantes);
        File.WriteAllText(path, updatedJsonDataString);
    } 
}

