using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTouch : MonoBehaviour
{
    public Transform respawnPoint; //Punto de aparicion del jugador
    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que toca es el jugador
        if (other.CompareTag("Player"))
        {
            // Encuentra el GameObject raíz del jugador
            GameObject playerRoot = other.gameObject;

            // Mueve al jugador al punto de reaparición
            CharacterController controller = playerRoot.GetComponent<CharacterController>();
            if (controller != null)
            {
                // Desactiva temporalmente el CharacterController para evitar conflictos al moverlo
                controller.enabled = false;

                // Mueve al jugador al punto de reaparición
                playerRoot.transform.position = respawnPoint.position;

                // Reactiva el CharacterController
                controller.enabled = true;
            }
        }
    }

    ////Este si quiero hacer colisiones normales
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        SceneManager.LoadScene(0);
    //    }
    //}
}
