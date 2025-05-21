using UnityEngine;

public class FireDamage : MonoBehaviour
{
    public float damagePerSecond = 5f; // Daño por segundo

    private void OnTriggerStay(Collider other)
    {
        // Verifica si el objeto con el que colisiona tiene el componente AIEnemy
        AIEnemy enemy = other.GetComponent<AIEnemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damagePerSecond * Time.deltaTime);
        }
    }
}
