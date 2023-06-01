using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class health : MonoBehaviour
{
    public float PlayerHealth = 100f;
    public float max_health = 250f;

    public Text player;

    void start()
    {
        //player = GameObject.Find("/Player/Canvas/player").GetComponent<Text>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (PlayerHealth == 0) {
            player.text = "Do er död";
            Application.Quit();
        }
        PlayerHealth = Mathf.Clamp(PlayerHealth, 0f, max_health);
        player.text = "Health: " + PlayerHealth.ToString();

    }
}
