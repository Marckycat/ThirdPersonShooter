using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GrabItem : MonoBehaviour
{
    [Header("Player Attributes")]
    [SerializeField] private int life; //Vida actual
    [SerializeField] private int shield; //Escudo actual
    [SerializeField] private int maxShield = 100; //Maximo del escudo
    [SerializeField] private float shieldRegenRate = 2f; //Regeneracion por segundo
    [SerializeField] private float shieldCooldown = 1f; //Tiempo antes de regenerar el escudo

    [Header("Ammo Attributes")]
    [SerializeField] private int maxAmmo = 50; //Maxima cantidad de balas
    private int currentAmmo; //Balas actuales

    private float lastDamageTime; //Ultimo momento en que el jugador recibe damage
    private float shieldRegenTimer = 0; //Temporizador para controlar los intervalos de regeneracion

    private CharacterController playerController;
    private void Start()
    {
        life = 100;
        shield = maxShield; //Inicializa el escudo al maximo
        currentAmmo = maxAmmo; // Inicializa las balas al máximo

        UIController.Instance.UpdateLife(life);
        UIController.Instance.UpdateShield(shield);
        UIController.Instance.UpdateAmmo(currentAmmo);

        playerController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        RegenerateShield();
        // Detectar disparo
        if (Input.GetButtonDown("Fire1")) // Usa "Fire1" para el clic izquierdo del mouse
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (currentAmmo > 0)
        {
            // Dispara y reduce la munición
            currentAmmo--;
            UIController.Instance.UpdateAmmo(currentAmmo);

            Debug.Log("Disparo realizado. Balas restantes: " + currentAmmo);

            // Aquí puedes agregar lógica para instanciar un proyectil o manejar el disparo
        }
        else
        {
            Debug.Log("Sin balas restantes");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Detecta si el jugador ha recogido vida
        if (other.transform.CompareTag("Life"))
        {
            life += 25;
            life = Mathf.Clamp(life, 0, 100); // Asegura que no exceda el máximo
            Destroy(other.gameObject);
            UIController.Instance.UpdateLife(life);
        }
        else if (other.transform.CompareTag("Ammo"))
        {
            // Recoge munición
            currentAmmo = Mathf.Clamp(currentAmmo + 10, 0, maxAmmo);
            Destroy(other.gameObject);
            UIController.Instance.UpdateAmmo(currentAmmo);
        }

    }


    public void TakeDamage(int damage)
    {
        lastDamageTime = Time.time; //Registra el momento del damage
        //Registrar el damage recibido
        if (shield > 0)
        {
            if (damage <= shield)
            {
                shield -= damage; //Reducir el escudo
            }
            else
            {
                int leftoverDamage = damage - shield;
                shield = 0;
                life -= leftoverDamage; //Pasar el damage sobrante a la vida
            }
        }
        else
        {
            life -= damage; //Reducir la vida si no hay escudo
        }

        // Limita la vida y el escudo para que no sean negativos
        shield = Mathf.Clamp(shield, 0, maxShield);
        life = Mathf.Clamp(life, 0, 100);

        //Actualizar la UI
        UIController.Instance.UpdateLife(life);
        UIController.Instance.UpdateShield(shield);

        //Verificar si el jugador ha muerto
        if (life <= 0)
        {
            PlayerDied();
        }
    }

    private void RegenerateShield()
    {
        //Verificar si el escudo no esta lleno y ha pasado suficiente tiempo desde el ultimo damage
        if (Time.time - lastDamageTime >= shieldCooldown && shield < maxShield)
        {
            //Incrementa el temporizador
            shieldRegenTimer += Time.deltaTime;

            //Si el temporizador alcanza un intervalo especifico, regenera el escudo
            if(shieldRegenTimer >= 2f)
            {
                shield += Mathf.CeilToInt(shieldRegenRate); //Regenerar en unidades fijas
                shield = Mathf.Clamp(shield, 0, maxShield); //Asegurar que no se exceda el maximo

                UIController.Instance.UpdateShield(shield); //Actualizar la UI
                shieldRegenTimer = 0f; //Reiniciar el temporizador
            }
        }
        else
        {
            //Reiniciar el temporizador si el escudo no esta regenerandose
            shieldRegenTimer = 0f;
        }
    }

    void PlayerDied()
    {
        // Desactiva los movimientos del jugador
        if (playerController != null)
        {
            playerController.enabled = false;
        }

        // Destruye el GameObject después de un breve tiempo (opcional)
        Destroy(gameObject, 1f);

        // Regresa al menú principal
        StartCoroutine(LoadMenu());
    }

    private IEnumerator LoadMenu()
    {
        yield return new WaitForSeconds(0f); // Espera unos segundos antes de regresar al menú
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver"); // Reemplaza "MainMenu" por el nombre exacto de tu escena del menú
    }
}
