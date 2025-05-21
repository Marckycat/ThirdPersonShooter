using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class WinTrigger : MonoBehaviour
{
    public float delayTime = 15f; // Tiempo de espera antes de cambiar de escena
    private bool isTriggered = false; // Asegura que no se repita la acción

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isTriggered) // Comprueba si ya fue activado
            {
                isTriggered = true; // Evita que la acción se repita
                StartCoroutine(WaitAndLoadScene());
            }
        }
    }

    private IEnumerator WaitAndLoadScene()
    {
        // Opcional: Aquí puedes agregar un efecto visual o un mensaje
        Debug.Log("Esperando " + delayTime + " segundos para cargar la escena de victoria...");

        yield return new WaitForSeconds(delayTime); // Espera el tiempo especificado

        SceneManager.LoadScene("Victory"); // Carga la escena de victoria
    }

}
