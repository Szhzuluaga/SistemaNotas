using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class VerificarUbicacion : MonoBehaviour
{
    public List<DragAndDrop> objetosArrastrables;
    public GameObject FondoFelicitaciones;
    public Text Felicitaciones;
    public Button Continuar;



    void Update()
    {
        EncontrarObjetosArrastrables();
        bool todosUbicados = VerificarUbicacionCorrecta();

        if (todosUbicados)
        {
            FondoFelicitaciones.SetActive(true);
            Felicitaciones.gameObject.SetActive(true);
            Continuar.gameObject.SetActive(true);
        }
        else
        {
            FondoFelicitaciones.SetActive(false);
            Felicitaciones.gameObject.SetActive(false);
            Continuar.gameObject.SetActive(false);
            
        }
    }


    private void EncontrarObjetosArrastrables()
    {
        objetosArrastrables = new List<DragAndDrop>((FindObjectsOfType<DragAndDrop>()));
       
    }




    private bool VerificarUbicacionCorrecta()
    {
        foreach (DragAndDrop objeto in objetosArrastrables)
        {
            if (!objeto.UbicadoCorrectamente)
            {
                return false;
            }
        }
        return true;
    }
    public void VolverAEscena1()
    {
        Application.LoadLevel("Escena1");
    }

}
