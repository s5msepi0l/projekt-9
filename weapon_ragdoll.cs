using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon_ragdoll : MonoBehaviour
{
    public GameObject weapon;
    public weapons inventory;
    public Rigidbody rb;

    void Start()
    {
        //rb = GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider other) 
    {
        Debug.Log(other.name);
        if (other.name == "Player") {
            inventory.index = Mathf.Clamp(inventory.index, -1, 9);

            inventory.Inventory[++inventory.index] = weapon;
            Destroy(gameObject);
        }
    }
}
