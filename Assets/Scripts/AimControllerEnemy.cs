using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimControllerEnemy : MonoBehaviour
{
    public float rotationSpeed;
    public float vectorDistance;
    public GameObject player;
    public bool isActive;
    private float singleStep;
    private Vector3 targetDirectionVector;
    private Vector3 newDirection;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void LateUpdate()
    {
        if(isActive){
        targetDirectionVector = player.transform.position - transform.position;
        singleStep = rotationSpeed * Time.deltaTime;

        newDirection = Vector3.RotateTowards(
            transform.forward,
            targetDirectionVector,
            singleStep,
            vectorDistance
        );

        Debug.DrawRay(transform.position, newDirection, Color.red);
        transform.rotation = Quaternion.LookRotation(newDirection);
            
        }
    }
}