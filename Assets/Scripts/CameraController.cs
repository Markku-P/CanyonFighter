using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public Vector3 offset;
    public float offsetX;
    public float offsetY;
    public float offsetZ;

    void Start ()
    {
        offset = transform.position - player.transform.position;
        offsetX = transform.position.x - player.transform.position.x;
        offsetY = transform.position.y - player.transform.position.y;
        offsetZ = transform.position.z - player.transform.position.z;
    }

    void LateUpdate ()
    {
        //transform.position = player.transform.position + offset;
        
        transform.position = new Vector3(
            player.transform.position.x + offsetX, //offsetX,
            offsetY, //20.3f,
            offsetZ  //18.8f
        );

        // Get player position
        Vector3 targetDirection = player.transform.position - transform.position;

        // The step size is equal to speed times frame time.
        float singleStep = speed * Time.deltaTime;

        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(
            transform.forward,
            targetDirection,
            singleStep,
            300.0f
            );

        // Draw a ray pointing at our target in
        Debug.DrawRay(transform.position, newDirection, Color.red);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        //transform.rotation = Quaternion.LookRotation(newDirection);
    }
}