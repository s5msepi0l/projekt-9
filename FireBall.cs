using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject character;
    public float Damage = 100f;
    public float Blast_range = 15f;
    public float Blast_amount = 20f;

    private Enemy enemy;
    private Movement character_rb;

    void OnTriggerEnter(Collider other) 
    {
        Debug.Log(other.gameObject.layer);
        switch(other.name)
        {
            case "enemy":
                other.transform.gameObject.GetComponent<Enemy>().health -= Damage;
                Destroy(gameObject);
            break;            

            case "Player":
                other.transform.gameObject.GetComponent<health>().PlayerHealth -= Damage / 4;
            break;

            default:
                break;
        }        

        if (other.gameObject.layer == 3 || other.gameObject.layer == 6  ) {
            float dist = Vector3.Distance(character.transform.position, transform.position);
            if (dist <= 5) {
                character_rb = character.GetComponent<Movement>();
                GameObject fpsCam = character.transform.GetChild(0).gameObject;
                Vector3 forward = fpsCam.transform.TransformDirection(Vector3.forward) * -((Mathf.Sqrt((Blast_range- dist) * Blast_amount)));
                character_rb.velocity = new Vector3(forward.x, forward.y, forward.z);
                
                character.GetComponent<health>().PlayerHealth -= Damage / 10;
                //character_rb.velocity = new Vector3(forward * 10);
            }

            Destroy(gameObject);
        }
    }
}
