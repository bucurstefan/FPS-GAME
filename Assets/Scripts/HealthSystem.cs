using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth = 100;

    // Other variables and methods...

    public void AdjustCurrentHealth(int amount)
    {
        currentHealth += amount;

        if (currentHealth <= 0)
        {
            // Destroy the GameObject when health reaches 0
            Destroy(gameObject);
        }
    }
}

