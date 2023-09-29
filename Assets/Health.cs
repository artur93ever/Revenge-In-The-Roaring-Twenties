using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public float deathTime;
    public Slider healthSlider; // Reference to the health slider
    public GameObject healthBar; // Reference to the health bar object

    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth; // Set the max value of the slider to maxHealth
        healthSlider.value = currentHealth; // Set the current value of the slider to currentHealth
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthSlider.value = currentHealth; // Update the slider value
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Perform death-related actions here
        healthBar.SetActive(false); // or Destroy(healthBar);
        Invoke("DestroyObject", deathTime); // Destroy the object after 2 seconds
    }

    void DestroyObject()
    {
        Destroy(gameObject);
    }
}
