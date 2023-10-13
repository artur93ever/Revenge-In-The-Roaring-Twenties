using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponSwitch : MonoBehaviour
{
    public InputAction interactAction;
    public GameObject parentObject;
    public GameObject weaponPosition;

    private bool insideTrigger = false;
    private bool hasGloves = false;

    // Start is called before the first frame update
    void Start()
    {
        interactAction.performed += OnInteract;
        interactAction.Enable();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started && insideTrigger)
        {
            InteractWeapon();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WeaponSwitch")) // Replace with the tag of your trigger collider
        {
            insideTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("WeaponSwitch")) // Replace with the tag of your trigger collider
        {
            insideTrigger = false;
        }
    }

    private void InteractWeapon()
    {
        UnityEngine.Debug.Log("Interact Button");

        GameObject gloves = GameObject.FindWithTag("GlovesWeapon");
        if (gloves != null)
        {
            Destroy(gloves);
        }

        // Loop through the children of the parent object
        foreach (Transform child in parentObject.transform)
        {
            // Check if the child is the prefab you're interested in
            if (child.CompareTag("KatanaWeapon")) // Replace with the tag of your prefab
            {
                // Instantiate the prefab at a specific position
                GameObject instantiatedPrefab = Instantiate(child.gameObject, weaponPosition.transform.position, Quaternion.identity);

                // Set the weaponPosition as the parent of the instantiated prefab
                instantiatedPrefab.transform.SetParent(weaponPosition.transform);

                // Remove the child from the parent object
                Destroy(child.gameObject);
            }
        }
    }



}
