using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; }
    [SerializeField] private TMP_Text lifeText; //Texto para la vida
    [SerializeField] private TMP_Text shieldText; //Texto para el escudo
    [SerializeField] private TMP_Text ammoText; //Texto para el escudo


    private void Awake()
    {
        if(Instance != null&& Instance != this)
        {
            Destroy(Instance.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void UpdateLife(int value)
    {
        lifeText.text = value.ToString();
    }

    public void UpdateShield(int shield)
    {
        shieldText.text = shield.ToString();
    }

    public void UpdateAmmo(int ammo)
    {
        ammoText.text = "Balas: " + ammo; // Asegúrate de que ammoText sea un campo asignado en el inspector
    }
}
