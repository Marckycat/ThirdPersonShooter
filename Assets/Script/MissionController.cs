using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MissionController : MonoBehaviour
{
    public int numDeObjetivos;
    public TextMeshProUGUI textoMision;

    public Transform destino; //Lugar al que se movera el siguiente objetivo
    public GameObject jugador; //Referencia al jugador
    private bool misionEnProgreso = true; //Estado de la mision

    void Start()
    {
        numDeObjetivos = GameObject.FindGameObjectsWithTag("Objetivo").Length;

        // Actualiza el texto de la misión
        ActualizarTextoMision();
    }

    void Update()
    {
        //Revisar si la mision de eliminacion de objetivos sigue activa
        if (misionEnProgreso)
        {
            //Revisar si los objetivos han sido eliminados
            CheckObjetivos();
        }
    }

    private void CheckObjetivos()
    {
        // Recalcula el número de enemigos restantes
        numDeObjetivos = GameObject.FindGameObjectsWithTag("Objetivo").Length;

        // Actualiza el texto de la misión
        ActualizarTextoMision();

        // Si todos los enemigos han sido eliminados, completa la misión
        if (numDeObjetivos == 0)
        {
            MisionIrADestino();
            //CompletarMision();
        }
    }
    private void ActualizarTextoMision()
    {
        if (misionEnProgreso)
        {
            textoMision.text = "Elimina los enemigos\nRestantes: " + numDeObjetivos;
        }
    }

    void MisionIrADestino()
    {
        misionEnProgreso = false; //Termina la fase de eliminacion de enemigos

        //Cambia el texto de la mision
        textoMision.text = "¡Mision completada!\nVe hacia tu destino.";

        ////Opcional: Activa un marcador visual en el destino
        //if(destino != null)
        //{
        //    ActivarMarcadorDestino();
        //}

        Debug.Log("Nueva mision: Ve hacia tu destino.");
    }

    //void ActivarMarcadorDestino()
    //{
    //    //Puedes agregar aqui un marcador visual en el destino (ejemplo, una flecha o luz)
    //    Debug.Log($"Marcador activado en: {destino.position}");
    //}

    private void OnTriggerEnter(Collider other)
    {
        // Detecta si el jugador ha llegado al destino
        if (other.CompareTag("Player"))
        {
            CompletarMisionFinal();
        }
    }

    void CompletarMisionFinal()
    {
        //Cambia el texto de la mision
        textoMision.text = "¡Has completado todas las misiones!";
        Debug.Log("¡Felicidades! Has completado el juego");

    }

}
