using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmunitionController : MonoBehaviour
{
    public int maxAmmunition = 50;
    private int currentAmmunition;
    public TextMeshProUGUI ammunitionText; // Reference to your TextMeshProUGUI component
    public float reloadTime = 2f; // Time it takes to reload in seconds

    private bool isReloading = false;
    private float reloadTimer = 0f;

    private void Start()
    {
        currentAmmunition = maxAmmunition;
        UpdateAmmunitionUI();
    }

    private void Update()
    {
        if (isReloading)
        {
            reloadTimer += Time.deltaTime;

            if (reloadTimer >= reloadTime)
            {
                FinishReload();
            }
        }
    }

    public bool CanShoot()
    {
        return currentAmmunition > 0 && !isReloading;
    }

    public void Shoot()
    {
        if (CanShoot())
        {
            currentAmmunition--;
            UpdateAmmunitionUI();
            // Add code here to actually shoot the bullet
        }
    }

    public void StartReload()
    {
        if (!isReloading && currentAmmunition < maxAmmunition)
        {
            isReloading = true;
            reloadTimer = 0f;
            // Add any visual or audio feedback for reloading here
        }
    }

    private void FinishReload()
    {
        currentAmmunition = maxAmmunition;
        isReloading = false;
        UpdateAmmunitionUI();
        // Add any visual or audio feedback for finishing reload here
    }

    private void UpdateAmmunitionUI()
    {
        ammunitionText.text = "AMMO: " + currentAmmunition + " / " + maxAmmunition;
    }
}
