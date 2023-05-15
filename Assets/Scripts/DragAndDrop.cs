using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Collections;
public class DragAndDrop : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 initialPosition;
    private Vector3 offset;
    public GameObject TextoEstudiante;
    public GameObject MensajeError;
    public GameObject MensajeAmbasPosiciones;

    private const string NombreArchivo = "/estudiantes.json";
    private DatoEstudiante est;
    public int Est;
    private bool EstaEnBorde = false;
    public bool UbicadoCorrectamente = false;
 
    
    void Start()
    {
        this.gameObject.SetActive(true);
        string path = Application.streamingAssetsPath + NombreArchivo;
        string estudiantesString = File.ReadAllText(path);
        DatosEstudiantes datosEstudiantes = JsonUtility.FromJson<DatosEstudiantes>(estudiantesString);
        est = datosEstudiantes.estudiantes[Est];
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButton(0))
        {
            isDragging = true;
            initialPosition = transform.position;
            offset = initialPosition - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            TextoEstudiante.SetActive(true);
            MensajeAmbasPosiciones.SetActive(false);
            MensajeError.SetActive(false);
            
        }
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            transform.position = new Vector3(newPosition.x, newPosition.y, initialPosition.z);
        }
    }

    private void OnMouseUp()
    {
        TextoEstudiante.SetActive(false);
        isDragging = false;
        EstaEnBorde = false;

        StartCoroutine(DelayedCollisionCheck());


    }
    private IEnumerator DelayedCollisionCheck()
    {
        yield return new WaitForFixedUpdate(); // Esperar hasta la próxima actualización de física para garantizar que se hayan resuelto las colisiones

        CheckCollision();
    }

    private void CheckCollision()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, transform.localScale, 0f);
        bool isInZonaAprobado = false;
        bool isInZonaReprobado = false;

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("ZonaAprobado"))
            {
                isInZonaAprobado = true;
            }
            else if (collider.CompareTag("ZonaReprobado"))
            {
                isInZonaReprobado = true;
            }
            else if (collider.CompareTag("Borde"))
            {
                EstaEnBorde = true;
            }
            
        }


        if (isInZonaAprobado && isInZonaReprobado)
        {
            MensajeAmbasPosiciones.SetActive(true);
            UbicadoCorrectamente = false;
            
        }
        else
        {
            MensajeAmbasPosiciones.SetActive(false);
        }

        if (isInZonaAprobado)
        {
            if (est.Nota < 3.0f)
            {
                MensajeError.SetActive(true);
                UbicadoCorrectamente = false;
            }
            else
            {
                MensajeError.SetActive(false);
            }
        }
        else if (isInZonaReprobado)
        {
            if (est.Nota >= 3.0f)
            {
                
                MensajeError.SetActive(true);
                
            }
            else
            {
                MensajeError.SetActive(false);
            }
        }
        if (isInZonaAprobado && est.Nota >= 3.0f && !isInZonaReprobado)
        {
            
            UbicadoCorrectamente = true;
        }
        else if (isInZonaReprobado && est.Nota < 3.0f && !isInZonaAprobado)
        {
            UbicadoCorrectamente = true;
            
        }
        else
        {
            UbicadoCorrectamente = false;
        }
        if (EstaEnBorde)
        {
            transform.position = initialPosition;
            
            EstaEnBorde = false;
        }
    }

}