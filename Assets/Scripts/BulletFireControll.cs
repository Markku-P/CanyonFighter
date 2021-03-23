using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFireControll : MonoBehaviour
{
    public float FiringSpeed;
    public bool isActive;
    private bool isFiring;
    public GameObject bullet;

    void Start(){
        isFiring = false;
    }

    void Update(){
        if (isActive){
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
        // Spawn new bullet
        GameObject newBullet = Instantiate(bullet);
        newBullet.transform.position = transform.position;
        newBullet.transform.rotation = transform.rotation;
    }
}