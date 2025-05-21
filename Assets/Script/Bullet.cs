using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Bullet : MonoBehaviour
{
    public float damage = 20f;//Damage de la bala inflige al enemigo

    private Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(this.gameObject, 10);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Verificar si el objeto golpeado tiene el componenete AIEnemy
        AIEnemy enemy = other.GetComponent<AIEnemy>();
        if(enemy != null)
        {
            enemy.TakeDamage(damage); //Aplicar el damage al enemigo
        }

        //Destruir la bala al impactar
        Destroy(gameObject);   
    }
}
