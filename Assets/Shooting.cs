using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform gunTransform;
    public float bulletSpeed = 15f;

    public AmmunitionController ammunitionController; // Reference to your ammunition controller

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && ammunitionController.CanShoot()) // Left mouse button and enough ammo
        {
            ammunitionController.Shoot();
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, gunTransform.position, gunTransform.rotation);
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        bulletRigidbody.velocity = gunTransform.forward * bulletSpeed;
    }
}
