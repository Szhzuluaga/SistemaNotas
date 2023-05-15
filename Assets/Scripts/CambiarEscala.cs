using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;
using System.IO;

public class CambiarEscala : MonoBehaviour
{
    private const string NombreArchivo = "/estudiantes.json";
    private const string ScaleKey = "ScaleKey";
    public InputField Nota1;
    public InputField Nota2;
    public InputField Nota3;
    public InputField Nota4;
    public InputField Nota5;
    public bool Scale;
    public DatosEstudiantes estudiantes;
    private List<DatoEstudiante> estudiantesList;
   

    private void Start()
    {
        // Recuperar el valor guardado de Scale utilizando PlayerPrefs
        if (PlayerPrefs.HasKey(ScaleKey))
        {
            Scale = PlayerPrefs.GetInt(ScaleKey) == 1;
        }
        else
        {
            Scale = true; // Valor predeterminado
        }
    }

    public void OnClick()
    {
        string path = Application.streamingAssetsPath + NombreArchivo;

        // Cargar los datos actualizados desde el JSON
        string estudianteString = File.ReadAllText(path);
        estudiantes = JsonUtility.FromJson<DatosEstudiantes>(estudianteString);
        estudiantesList = estudiantes.estudiantes;

        if (Scale)
        {
            Scale = false;

            for (int i = 0; i < estudiantesList.Count; i++)
            {
                float nota = estudiantesList[i].Nota;
                nota *= 100f / 5f;
                SetNotaText(i, nota);
                estudiantesList[i].Nota = float.Parse(GetNotaText(i), CultureInfo.InvariantCulture);
            }
        }
        else
        {
            Scale = true;

            for (int i = 0; i < estudiantesList.Count; i++)
            {
                float nota = estudiantesList[i].Nota;
                nota *= 5f / 100f;
                SetNotaText(i, nota);
                estudiantesList[i].Nota = float.Parse(GetNotaText(i), CultureInfo.InvariantCulture);
            }
        }

        // Guardar los datos actualizados en el JSON
        string updatedJsonDataString = JsonUtility.ToJson(estudiantes);
        File.WriteAllText(path, updatedJsonDataString);

        // Guardar el valor actual de Scale utilizando PlayerPrefs
        PlayerPrefs.SetInt(ScaleKey, Scale ? 1 : 0);
    }

    private string GetNotaText(int index)
    {
        switch (index)
        {
            case 0: return Nota1.text;
            case 1: return Nota2.text;
            case 2: return Nota3.text;
            case 3: return Nota4.text;
            case 4: return Nota5.text;
            default: return "";
        }
    }

    private void SetNotaText(int index, float nota)
    {
        string notaText = nota.ToString("N", CultureInfo.InvariantCulture);

        switch (index)
        {
            case 0: Nota1.text = notaText; break;
            case 1: Nota2.text = notaText; break;
            case 2: Nota3.text = notaText; break;
            case 3: Nota4.text = notaText; break;
            case 4: Nota5.text = notaText; break;
        }
    }
}
