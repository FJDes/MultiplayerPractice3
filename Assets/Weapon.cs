using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int damage;

    public Camera camera;

    public float fireRate;

    [Header("VFX")]
    public GameObject hitVFX;

    private float nextFire;

    [Header("Ammo")]
    public int mag = 5;
    public int ammo = 30;
    public int magAmmo = 30;

    [Header("UI")]
    public TextMeshProUGUI magText;
    public TextMeshProUGUI ammoText;

    void Update()
    {
        if (nextFire > 0) {
            nextFire -= Time.deltaTime;
        }

        if (Input.GetButton("Fire1") && nextFire <= 0 && ammo > 0)
        {
            nextFire = 1 / fireRate;

            ammo--;

            UI_PrintAmmoAndMag();

            Fire();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    void Reload() 
    {
        if (mag > 0)
        {
            mag--;
            ammo = magAmmo;
        }

        UI_PrintAmmoAndMag();
    }

    void UI_PrintAmmoAndMag() {
        magText.text = mag.ToString();
        ammoText.text = ammo + "/" + magAmmo;
    }

    void Fire()
    {
        Ray ray = new Ray(camera.transform.position, camera.transform.forward);

        RaycastHit hit;

        if (Physics.Raycast(ray.origin, ray.direction, out hit, 100f)) 
        {
            PhotonNetwork.Instantiate(hitVFX.name, hit.point, Quaternion.identity);
            
            if (hit.transform.gameObject.GetComponent<Health>())
            {
                hit.transform.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, damage);
            }
        }
    }
}
