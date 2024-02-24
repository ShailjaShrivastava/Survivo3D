using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBarImage; // Reference to the UI image representing the health bar
    public Transform target; // Reference to the object whose health this bar represents (Tower in this case)

    private float originalWidth; // Initial width of the health bar image

    void Start()
    {
     
    }

    void Update()
    {
        if (target == null) // Handle missing target
        {
            return;
        }

        float healthPercentage = target.GetComponent<Tower>().health / target.GetComponent<Tower>().maxHealth;
        healthBarImage.value = healthPercentage;
    }

    public void UpdateHealthBar(float healthPercentage)
    {
        // This allows setting health percentage directly, useful for external updates
     
    }
}

