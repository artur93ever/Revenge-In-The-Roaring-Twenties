using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class CollideAttack : MonoBehaviour
{
    public GameObject[] attackColliders;

    void Start()
    {
        // Disable all attack colliders at the start
        DisableAllColliders();
    }

    void Update()
    {
        // Your update logic here
    }

    public void EnableAllColliders()
    {
        foreach (GameObject collider in attackColliders)
        {
            if (!collider.activeSelf)
            {
                UnityEngine.Debug.Log("Activating collider: " + collider.name);
                collider.SetActive(true);
            }
        }
    }

    public void DisableAllColliders()
    {
        foreach (GameObject collider in attackColliders)
        {
            collider.SetActive(false);
        }
    }
}
