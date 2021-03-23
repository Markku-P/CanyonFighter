using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireController : MonoBehaviour
{
    public float bulletSpeed;
    public float FiringSpeed;
    public bool autoFire;
    public bool isActive;
    public GameObject bullet;
    private bool isFiring;


    void Start(){
        isFiring = false;
    }


    void Update(){
        if (Input.GetKeyDown(KeyCode.Return)){
           //autoFire = true;
           Fire();
        }

        // Auto fire
        if (autoFire){
            if (!isFiring){
            isFiring = true;
            InvokeRepeating("Fire", 0f, FiringSpeed);
            }
        }
        else{
            if(isFiring){
                isFiring = false;
                CancelInvoke("Fire");
            }
        }
    }

    void Fire(){
        if (isActive){
            // Spawn new bullet
            GameObject newBullet = Instantiate(bullet);
            newBullet.transform.position = transform.position;
            //newBullet.transform.rotation = transform.rotation;
        }
    }
}