using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    
    private int currentWeapon = 0;

    [Space]
    public Weapon[] weapons;
    
    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log("IT CAAALLLLEEED!");
        weapons[0].gameObject.SetActive(true);
        
        int i = 1;
        foreach (Weapon _weapon in weapons) {
            weapons[i].gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        SelectWeapon();
    }


    void SelectWeapon() 
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            Debug.Log("Press W");
            ActivateWeapon(0, currentWeapon);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            Debug.Log("Press X");
            ActivateWeapon(1, currentWeapon);
        }
    
    }

    void ActivateWeapon(int toActivate, int toDisable) {
        if (toActivate != toDisable) {
            weapons[toDisable].gameObject.SetActive(false);
            weapons[toActivate].gameObject.SetActive(true);
            currentWeapon = toActivate;
        }
    }
}
