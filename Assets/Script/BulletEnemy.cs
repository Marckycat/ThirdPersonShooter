using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    [SerializeField] private int damage = 10; //Damage que causa el proyectil
    [SerializeField] private float lifeTime = 5f; //Tiempo antes de que el projectil desaparezca

    // Start is called before the first frame update
    void Start()
    {
        //Destruir el proyectil despues de cierto tiempo
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Verificar si el projectile golpeo al jugador
        if (collision.transform.CompareTag("Player"))
        {
            //Llamar el metodo TakeDamage del jugador
            GrabItem player = collision.transform.GetComponent<GrabItem>();
            if(player != null)
            {
                player.TakeDamage(damage);
            }
            //Destruir el proyectil tras colisionar
            Destroy(gameObject);
        }
        //else
        //{
        //    //Opcional: Destruir el proyectil si golpea otra cosa
        //    Destroy(gameObject);
        //}
    }
}
