using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Deagle : MonoBehaviour
{
    public float Range = 100f;
    public float damage = 50f;
    public float magazine = 7f;
    public float fire_rate = 0.05f;

    public Enemy enemy;
    [SerializeField] LineRenderer lineRend;
    public GameObject controller;
    public GameObject fpsCam;
    public Text ammo_text;


    private float Ammo;
    private bool allow_fire;
    private bool reloading;

    void Start()
    {
        reloading = false;
        allow_fire = true;
        Ammo = magazine;
        ammo_text.text = "Ammo: " + magazine.ToString();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Ammo >= 1 && allow_fire && !reloading) {
            StartCoroutine(shoot(fire_rate));
            Ammo -= 1;
            ammo_text.text = "Ammo: " + Ammo.ToString();
        }

        if (Input.GetKeyDown(KeyCode.R) && !reloading) {
            StartCoroutine(reload(1f));
        }
    }


    IEnumerator shoot(float wait_period)
    {
        allow_fire = false;
        
        Ray ray = new Ray(fpsCam.transform.position, fpsCam.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Range)) {
            lineRend.enabled = true;
            lineRend.SetPosition(0, transform.position);
            lineRend.SetPosition(1, hit.point);
        
            StartCoroutine(bullet_show_dropoff(0.03f));
            if (hit.transform.gameObject.name == "enemy") {
                enemy.health -= damage;
            }        
        }
        yield return new WaitForSeconds(wait_period);
        allow_fire = true;
    }

    IEnumerator reload(float wait_period)
    {
        reloading = true;
        ammo_text.text = "Reloading...";
        yield return new WaitForSeconds(wait_period);
        Ammo = magazine;
        reloading = false;
        ammo_text.text = "Ammo: " + magazine.ToString();
    }
        
    IEnumerator bullet_show_dropoff(float wait_period)
    {
        yield return new WaitForSeconds(wait_period);
        lineRend.enabled = false;
    }
}
