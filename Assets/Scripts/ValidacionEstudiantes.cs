using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;

public class ValidacionEstudiantes : MonoBehaviour
{
    private const string NombreArchivo = "/estudiantes.json";
    public Toggle ToggleEstudiante1;
    public Toggle ToggleEstudiante2;
    public Toggle ToggleEstudiante3;
    public Toggle ToggleEstudiante4;
    public Toggle ToggleEstudiante5;
    public DatosEstudiantes estudiantes;
    private List<DatoEstudiante> estudiantesList;
    public CambiarEscala cambiarEscalaScript; // Referencia al script CambiarEscala
    public Text RecuadroError;
    public Button Continuar;

    public void OnClickValidacion()
    {
        string path = Application.streamingAssetsPath + NombreArchivo;

        // Cargar los datos actualizados desde el JSON
        string estudianteString = File.ReadAllText(path);
        estudiantes = JsonUtility.FromJson<DatosEstudiantes>(estudianteString);
        estudiantesList = estudiantes.estudiantes;

        bool errorEncontrado = false;
        float notaAprobatoria = 0.0f;

        if (cambiarEscalaScript != null)
        {
            if (cambiarEscalaScript.Scale)
            {
                // Escala de 0-5
                notaAprobatoria = 3.0f;
            }
            else
            {
                // Escala de 0-100
                notaAprobatoria = 60.0f;
            }
        }

        if (ToggleEstudiante1.isOn)
        {
            if (GetNotaEstudiante(estudiantesList, 0) < notaAprobatoria)
            {
                errorEncontrado=Error(estudiantesList, 0, 0, notaAprobatoria);

            }

        }
        else if (GetNotaEstudiante(estudiantesList, 0) >= notaAprobatoria)
        {
            errorEncontrado = Error(estudiantesList, 0, 1, notaAprobatoria);
        }

        if (ToggleEstudiante2.isOn)
        {
            if (GetNotaEstudiante(estudiantesList, 1) < notaAprobatoria)
            {
                errorEncontrado = Error(estudiantesList, 1, 0, notaAprobatoria);

            }

        }
        else if (GetNotaEstudiante(estudiantesList, 1) >= notaAprobatoria)
        {
            errorEncontrado = Error(estudiantesList, 1, 1, notaAprobatoria);
        }

        if (ToggleEstudiante3.isOn)
        {
            if (GetNotaEstudiante(estudiantesList, 2) < notaAprobatoria)
            {
                errorEncontrado = Error(estudiantesList, 2, 0, notaAprobatoria);
            }

        }
        else if (GetNotaEstudiante(estudiantesList, 2) >= notaAprobatoria)
        {
            errorEncontrado = Error(estudiantesList, 2, 1, notaAprobatoria);
        }

        if (ToggleEstudiante4.isOn)
        {
            if (GetNotaEstudiante(estudiantesList, 3) < notaAprobatoria)
            {
                errorEncontrado = Error(estudiantesList, 3, 0, notaAprobatoria);

            }

        }
        else if (GetNotaEstudiante(estudiantesList, 3) >= notaAprobatoria)
        {
            errorEncontrado = Error(estudiantesList, 3, 1, notaAprobatoria);
        }

        if (ToggleEstudiante5.isOn)
        {
            if (GetNotaEstudiante(estudiantesList, 4) < notaAprobatoria)
            {
                errorEncontrado = Error(estudiantesList, 4, 0, notaAprobatoria);

            }

        }
        else if (GetNotaEstudiante(estudiantesList, 4) >= notaAprobatoria)
        {
            errorEncontrado = Error(estudiantesList, 4, 1, notaAprobatoria);
        }

        if (!errorEncontrado)
        {
            RecuadroError.gameObject.SetActive(false);
            Continuar.gameObject.SetActive(true);
            
        }
    }

    private float GetNotaEstudiante(List<DatoEstudiante> estudiantesList, int numeroEstudiante)
    {
        if (numeroEstudiante >= 0 && numeroEstudiante < estudiantesList.Count)
        {
            return estudiantesList[numeroEstudiante].Nota;
        }

        return 0.0f;
    }
    private bool Error(List<DatoEstudiante> estudiantesList,int numeroEstudiante, int Errorcode, float notaAprobatoria)
    {
        bool errorEncontrado = false;
        if (Errorcode == 0)
        {
            RecuadroError.gameObject.SetActive(true);
            Continuar.gameObject.SetActive(false);
            RecuadroError.text = "Error en " + estudiantesList[numeroEstudiante].Nombre + " nota inferior a " + notaAprobatoria + " y marcado como aprobado.";
            errorEncontrado = true;
        }
        else
        {
            RecuadroError.gameObject.SetActive(true);
            Continuar.gameObject.SetActive(false);
            RecuadroError.text = "Error en " + estudiantesList[numeroEstudiante].Nombre + " nota superior a " + notaAprobatoria + " y marcado como no aprobado.";
            errorEncontrado = true;
        }
        return errorEncontrado;
    }
}
