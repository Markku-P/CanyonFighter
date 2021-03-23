using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControllerBlock : MonoBehaviour
{ 
    public float setStartSpeedZ;
    public Vector3 setSpeed;
    public Vector3 speed;
    public Vector3 setRotationSpeed;
    public Vector3 rotationSpeed;
    public Vector3 velocitySmoothTime;
    public Vector3 rotationVelocitySmoothTime;
    private float xVelocity = 0.0f;
    private float yVelocity = 0.0f;
    private float zVelocity = 0.0f;
    private float xRotateVelocity = 0.0f;
    private float yRotateVelocity = 0.0f;
    private float zRotateVelocity = 0.0f;


    void Start()
    {
        // Set start speed
        setSpeed[2] = setStartSpeedZ;
        speed[2] = setStartSpeedZ;
    }

    void Update()
    {
        SmoothSpeedChanges();
    
        //transform.rotation = Quaternion.AngleAxis(30, Vector3.left);
        transform.Rotate(
            rotationSpeed[0] * Time.deltaTime,
            rotationSpeed[1] * Time.deltaTime,
            rotationSpeed[2] * Time.deltaTime
            );
    }

    void SmoothSpeedChanges()
    {
        // Smooth speed changes
        speed[0] = Mathf.SmoothDamp(
            speed[0],
            setSpeed[0],
            ref xVelocity,
            velocitySmoothTime[0]
        );

        speed[1] = Mathf.SmoothDamp(
            speed[1],
            setSpeed[1],
            ref yVelocity,
            velocitySmoothTime[1]
        );

        speed[2] = Mathf.SmoothDamp(
            speed[2],
            setSpeed[2],
            ref zVelocity,
            velocitySmoothTime[2]
        );

        rotationSpeed[0] = Mathf.SmoothDamp(
            rotationSpeed[0],
            setRotationSpeed[0],
            ref xRotateVelocity,
            rotationVelocitySmoothTime[0]
        );

        rotationSpeed[1] = Mathf.SmoothDamp(
            rotationSpeed[1],
            setRotationSpeed[1],
            ref yRotateVelocity,
            rotationVelocitySmoothTime[1]
        );

        rotationSpeed[2] = Mathf.SmoothDamp(
            rotationSpeed[2],
            setRotationSpeed[2],
            ref zRotateVelocity,
            rotationVelocitySmoothTime[2]
        );
    }
}