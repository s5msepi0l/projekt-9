using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healing : MonoBehaviour
{
    public health player;
    public float healing_factor = 100;
    public float wait_period = 10f;
    private bool CR_running;

    void Start() { CR_running = false;}

    void OnTriggerEnter(Collider other) 
    {
        if (CR_running == false) {
            StartCoroutine(waiter());
        }
        Debug.Log(CR_running);
    }

    IEnumerator waiter()
    {
        player.PlayerHealth += healing_factor;    
        
        CR_running = true;
        yield return new WaitForSeconds(wait_period);
        CR_running = false;
    }
}