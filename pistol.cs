using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pistol : MonoBehaviour
{
    public float Range = 100f;
    public float damage = 10f;
    public float magazine = 20f;
    public float fireball_speed = 75;
    public float ball_inaccuracy = 2f;
    public float bullet_inaccuracy = 0.2f;
    public Enemy enemy;

    [SerializeField] LineRenderer lineRend;
    public Transform gun;
    public GameObject controller;
    public GameObject fpsCam;
    public GameObject fireBall;
    public Text ammo;
    private float Ammo;
    private float explosiveAmmo;
    private bool reloading;
    private bool allow_fire;

    void Start()
    {
        reloading = false;
        Ammo = magazine;
        explosiveAmmo = magazine / 4;
        allow_fire = true;
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Ammo >= 1 && (allow_fire) && !reloading) {
            StartCoroutine(shoot(0.05f));
            Ammo -= 1;
        }

        if (Input.GetButtonDown("Fire2") && explosiveAmmo >= 1 && (allow_fire) && !reloading) {
            StartCoroutine(explo_shoot(0.1f));
            explosiveAmmo -= 1;
        }
        if (Input.GetKeyDown(KeyCode.R) && !reloading) {
            reload();
        }
        if (!reloading) {
            ammo.text = "Ammo: " + Ammo.ToString() + ", " + explosiveAmmo.ToString();
        } else
            ammo.text = "Reloading...";
    }


    void reload()
    {   
        if (Ammo >= 5) {
            StartCoroutine(timed_reload(1));
        } else {
            StartCoroutine(timed_reload(2));
        }
    }
    IEnumerator shoot(float wait_period) 
    {
        allow_fire = false;

        float accuracy = Random.Range(-bullet_inaccuracy, ball_inaccuracy);
        Ray ray = new Ray(fpsCam.transform.position, new Vector3(fpsCam.transform.forward.x + accuracy, fpsCam.transform.forward.y, fpsCam.transform.forward.z + accuracy));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Range)) {
            lineRend.enabled = true;
            lineRend.SetPosition(0, gun.position);
            lineRend.SetPosition(1, hit.point);

            StartCoroutine(bullet_show_dropoff(0.1f));
            if (hit.transform.gameObject.name == "enemy") {
                enemy.health -= damage;
            }
            Vector3 forward = fpsCam.transform.TransformDirection(Vector3.forward) * 10;
            Debug.DrawRay(fpsCam.transform.position, forward, Color.green, 10f);
    }

    yield return new WaitForSeconds(wait_period);
    allow_fire = true;
    }

    IEnumerator bullet_show_dropoff(float wait_period)
    {
        yield return new WaitForSeconds(wait_period);
        lineRend.enabled = false;
    }

    IEnumerator timed_reload(float wait_period)
    {
        reloading = true;
        ammo.text = "Reloading...";
        yield return new WaitForSeconds(wait_period);
        Ammo = magazine;
        explosiveAmmo = magazine / 4;
        reloading = false;
    }

    IEnumerator explo_shoot(float wait_period)
    {
        allow_fire = false;
        float accuracy = Random.Range(-ball_inaccuracy, ball_inaccuracy);

        Vector3 forward = fpsCam.transform.TransformDirection(Vector3.forward.x + accuracy, Vector3.forward.y, Vector3.forward.z + accuracy);
        GameObject ball = Instantiate(fireBall);
        ball.transform.position = new Vector3(fpsCam.transform.position.x, fpsCam.transform.position.y, fpsCam.transform.position.z);
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        ball.GetComponent<FireBall>().character = controller;

        rb.AddForce(forward * fireball_speed, ForceMode.Impulse);
        yield return new WaitForSeconds(wait_period);
        allow_fire = true;
    }
}