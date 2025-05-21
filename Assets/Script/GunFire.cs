using UnityEngine;

public class GunFire : MonoBehaviour
{
    public GameObject firePrefab; // Prefab del efecto de fuego
    public Transform firePoint; // Punto desde donde se lanza el fuego
    public float fireDuration = 0.5f; // Duración del efecto visual
    public float fireDamage = 5f; // Daño por segundo

    public float fireSpeed = 10f; // Velocidad del movimiento del fuego
    public float fireRange = 5f; // Alcance del lanzallamas

    //private float nextFireTime = 0f;
    public float fireRate = 0.1f; // Intervalo entre ticks de daño

    void Update()
    {
        // Activar el lanzallamas con el botón izquierdo del mouse
        if (Input.GetMouseButton(1))
        {
            FireFlamethrower();
        }
    }

    void FireFlamethrower()
    {
        // Instanciar el efecto del fuego en la posición y dirección del firePoint
        GameObject fireEffect = Instantiate(firePrefab, firePoint.position, firePoint.rotation);
        // Añadir movimiento hacia adelante si tiene un Rigidbody
        Rigidbody rb = fireEffect.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = firePoint.forward * fireSpeed;
        }
        Destroy(fireEffect, fireDuration); // Destruir el efecto después de un tiempo
    }

}
