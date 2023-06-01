using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boost : MonoBehaviour
{
    public Movement player;
    public Vector3 m_thrust;

    void OnTriggerEnter(Collider other) 
    {
        if (other.transform.gameObject.name == "model") {
            player.velocity += m_thrust;
        }
    }
}