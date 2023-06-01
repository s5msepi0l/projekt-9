using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class weapons : MonoBehaviour
{
    public Text Ammo;
    public Enemy enemy;
    public GameObject Controller;
    public GameObject FpsCam;
    public GameObject[] Inventory;
    public int index = 0;
    public int index_range = 5;

    private float current_index;
    private GameObject current_weapon;
    void Start()
    {
        current_weapon = new GameObject();
        Inventory = new GameObject[index_range];
    }

    // Update is called once per frame
    void Update()
    {
        if (index != -1) {

        if (Input.GetAxis("Mouse ScrollWheel") < 0 || Input.GetAxis("Mouse ScrollWheel") > 0) {
            current_index += Input.GetAxis("Mouse ScrollWheel") * 20f;
            current_index = Mathf.Clamp(current_index, 0, index);

            if (Inventory[(int)current_index] != null) {
                switch(Inventory[(int)current_index].name) {
                    case "thompson":
                        Destroy(current_weapon);
                        tommy_instantiate();
                        break;
                    case "Deagle":
                        Destroy(current_weapon);
                        deagle_instantiate();
                        break;
                    }
                } else {
                Destroy(current_weapon);
                }         
            } 
        }
    }

    void deagle_instantiate() {
        current_weapon = Instantiate(Inventory[(int)current_index], FpsCam.transform);
        
        current_weapon.GetComponent<Deagle>().enemy = enemy;
        current_weapon.GetComponent<Deagle>().controller = Controller;
        current_weapon.GetComponent<Deagle>().fpsCam = FpsCam;
        current_weapon.GetComponent<Deagle>().ammo_text = Ammo;   
    }

    void tommy_instantiate()
    {
        current_weapon = Instantiate(Inventory[(int)current_index], FpsCam.transform);

        current_weapon.GetComponent<pistol>().enemy = enemy;
        current_weapon.GetComponent<pistol>().fpsCam = FpsCam;
        current_weapon.GetComponent<pistol>().ammo = Ammo;
        current_weapon.GetComponent<pistol>().controller = Controller;
    }
}   
