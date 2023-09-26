using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public float deathTime;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Perform death-related actions here
        Invoke("DestroyObject", deathTime); // Destroy the object after 2 seconds
    }

    void DestroyObject()
    {
        Destroy(gameObject);
    }
}
