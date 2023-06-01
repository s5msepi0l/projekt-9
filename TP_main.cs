using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP_main : MonoBehaviour
{
    public Transform TP_position;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        other.transform.position = TP_position.position;
    }
}