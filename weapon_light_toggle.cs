using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon_light_toggle : MonoBehaviour
{
    public GameObject light;
    private bool isEnabled;

    void start() {
        isEnabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            isEnabled = !isEnabled;
            light.GetComponent<Light>().enabled = isEnabled;
        }
    }
}
