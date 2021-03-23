using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControllerBullet : MonoBehaviour
{
    public float speedX;
    public float speedY;
    public float speedZ;
    public float hitDamage;
    public GameObject bulletsStopPlane;
    public GameObject bulletsStopPlane2;
    void Start()
    {
        bulletsStopPlane = GameObject.FindWithTag("bulletsstopplane");
        bulletsStopPlane2 = GameObject.FindWithTag("bulletsstopplane2");
    }

    void Update()
    {
        transform.Translate(
            speedX * Time.deltaTime,
            speedY * Time.deltaTime,
            speedZ * Time.deltaTime
        );

        // Check if block is far enough to destroy
        if (gameObject.transform.position.z > (bulletsStopPlane.transform.position.z)){
            Destroy(gameObject);
        }
        else if (gameObject.transform.position.z < (bulletsStopPlane2.transform.position.z)){
            Destroy(gameObject);
        }
    }
}