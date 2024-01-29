using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 10;

    private void OnCollisionEnter(Collision collision)
    {
        Health health = collision.gameObject.GetComponent<Health>();

        if (health != null)
        {
            health.TakeDamage(damage);
        }

        Destroy(gameObject); // Destroy the bullet on collision
    }
}
