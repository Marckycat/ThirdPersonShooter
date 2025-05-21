using UnityEngine;

public class GunFire : MonoBehaviour
{
    public GameObject firePrefab; // Prefab del efecto de fuego
    public Transform firePoint; // Punto desde donde se lanza el fuego
    public float fireDuration = 0.5f; // Duraci�n del efecto visual
    public float fireDamage = 5f; // Da�o por segundo

    public float fireSpeed = 10f; // Velocidad del movimiento del fuego
    public float fireRange = 5f; // Alcance del lanzallamas

    //private float nextFireTime = 0f;
    public float fireRate = 0.1f; // Intervalo entre ticks de da�o

    void Update()
    {
        // Activar el lanzallamas con el bot�n izquierdo del mouse
        if (Input.GetMouseButton(1))
        {
            FireFlamethrower();
        }
    }

    void FireFlamethrower()
    {
        // Instanciar el efecto del fuego en la posici�n y direcci�n del firePoint
        GameObject fireEffect = Instantiate(firePrefab, firePoint.position, firePoint.rotation);
        // A�adir movimiento hacia adelante si tiene un Rigidbody
        Rigidbody rb = fireEffect.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = firePoint.forward * fireSpeed;
        }
        Destroy(fireEffect, fireDuration); // Destruir el efecto despu�s de un tiempo
    }

}
