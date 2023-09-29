using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Material whiteMaterial; // Assign your white material in the inspector

    private Renderer myRenderer;
    private Material originalMaterial;

    void Start()
    {
        myRenderer = GetComponent<Renderer>();
        originalMaterial = myRenderer.material;
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Gloves")
        {
            StartCoroutine(ChangeColorCoroutine());
        }
    }

    IEnumerator ChangeColorCoroutine()
    {
        myRenderer.material = whiteMaterial; // Change to white material

        yield return new WaitForSeconds(0.3f); // Wait for 0.3 seconds

        myRenderer.material = originalMaterial; // Revert to original material
    }
}
