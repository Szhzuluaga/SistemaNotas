using System.IO;
using UnityEngine;
using System.Collections.Generic;


public class SceneManager : MonoBehaviour
{
    private const string NombreArchivo = "/estudiantes.json";
    public GameObject DataEstudiante1;
    public GameObject DataEstudiante2;
    public GameObject DataEstudiante3;
    public GameObject DataEstudiante4;
    public GameObject DataEstudiante5;
    public GameObject TextoFijo;
    public GameObject Fondoyaubicado;
    public GameObject empezar;
    private bool HasChanged = false;



    private void Start()

    {
        DesactivarCampos();
        Cargar();
        int hasChangedValue = PlayerPrefs.GetInt("HasChanged", 0);
        HasChanged = hasChangedValue == 1;
    }
    void Update()
    {
        if (HasChanged)
        {
            Fondoyaubicado.SetActive(true);
        }
    }

    public void DesactivarCampos()
    {
        DataEstudiante1.SetActive(false);
        DataEstudiante2.SetActive(false);
        DataEstudiante3.SetActive(false);
        DataEstudiante4.SetActive(false);
        DataEstudiante5.SetActive(false);
        TextoFijo.SetActive(false);
    }
    public void ActivarEst1()
    {
        DataEstudiante1.SetActive(true);
        DataEstudiante2.SetActive(false);
        DataEstudiante3.SetActive(false);
        DataEstudiante4.SetActive(false);
        DataEstudiante5.SetActive(false);
        TextoFijo.SetActive(true);


    }
    public void ActivarEst2()
    {
        DataEstudiante1.SetActive(false);
        DataEstudiante2.SetActive(true);
        DataEstudiante3.SetActive(false);
        DataEstudiante4.SetActive(false);
        DataEstudiante5.SetActive(false);
        TextoFijo.SetActive(true);

    }
    public void ActivarEst3()
    {
        DataEstudiante1.SetActive(false);
        DataEstudiante2.SetActive(false);
        DataEstudiante3.SetActive(true);
        DataEstudiante4.SetActive(false);
        DataEstudiante5.SetActive(false);
        TextoFijo.SetActive(true);

    }
    public void ActivarEst4()
    {
        DataEstudiante1.SetActive(false);
        DataEstudiante2.SetActive(false);
        DataEstudiante3.SetActive(false);
        DataEstudiante4.SetActive(true);
        DataEstudiante5.SetActive(false);
        TextoFijo.SetActive(true);

    }
    public void ActivarEst5()
    {
        DataEstudiante1.SetActive(false);
        DataEstudiante2.SetActive(false);
        DataEstudiante3.SetActive(false);
        DataEstudiante4.SetActive(false);
        DataEstudiante5.SetActive(true);
        TextoFijo.SetActive(true);

    }
    public void SiguienteEscena()
    {
        HasChanged = true;
        PlayerPrefs.SetInt("HasChanged", HasChanged ? 1 : 0);
        PlayerPrefs.Save();

        // Cambiar a la siguiente escena
        Application.LoadLevel("Escena2");
    }

    public void VolverAEjecutar()
    {
        Fondoyaubicado.SetActive(false);
        HasChanged = false;
    }
    public void Finalizar()
    {
        Application.Quit();
    }
    private void OnApplicationQuit()
    {

        PlayerPrefs.SetInt("HasChanged", HasChanged ? 1 : 0);
        PlayerPrefs.Save();
    }
    public void Empezar()
    {
        empezar.SetActive(false);
        Fondoyaubicado.SetActive(false);
    }




    [SerializeField]
    private DatosEstudiantes datosEstudiantes;

    [ContextMenu("InizializarDatos")]
    public void InizializarDatos()
    {

        datosEstudiantes = new DatosEstudiantes();
        datosEstudiantes.estudiantes = new List<DatoEstudiante>();

        for (int i = 0; i < 5; i++)
        {

            DatoEstudiante estudiante = new DatoEstudiante();
            estudiante.Nombre = "Nombre" + (i + 1);
            estudiante.Apellido = "Apellido" + (i + 1);
            estudiante.Codigo = "Cedula";
            estudiante.Correo = "Correo@ejemplo.com";
            estudiante.Nota = Random.Range(0f, 5f);
            datosEstudiantes.estudiantes.Add(estudiante);

        }

        string estudiantesJSON = JsonUtility.ToJson(datosEstudiantes);
        Debug.Log(estudiantesJSON);

        string path = Application.streamingAssetsPath + NombreArchivo;
        File.WriteAllText(path, estudiantesJSON);
        DataEstudiante1.GetComponent<CargarDatosIniciales>().LoadJSON();
        DataEstudiante2.GetComponent<CargarDatosIniciales>().LoadJSON();
        DataEstudiante3.GetComponent<CargarDatosIniciales>().LoadJSON();
        DataEstudiante4.GetComponent<CargarDatosIniciales>().LoadJSON();
        DataEstudiante5.GetComponent<CargarDatosIniciales>().LoadJSON();
    }




    [ContextMenu("Load")]
    public void Cargar()
    {
        string path = Application.streamingAssetsPath + NombreArchivo;
        string estudiantesString = File.ReadAllText(path);
        DatosEstudiantes datosEstudiantes = JsonUtility.FromJson<DatosEstudiantes>(estudiantesString);
        DatoEstudiante est = datosEstudiantes.estudiantes[0];
    }
}