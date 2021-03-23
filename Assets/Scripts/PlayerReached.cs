using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReached : MonoBehaviour
{
    public float speedAfterPlayerReached;
    public float smoothTime;
    private float previousSmoothTime;
    private bool triggered = false;
    private Rigidbody player;
    private MovementControllerBlock movementcontrollerblock;
    private MovementControllerPlayer movementcontrollerplayer;

    void Start(){
        movementcontrollerblock = GameObject.FindWithTag("movementcontrollerblock").GetComponent<MovementControllerBlock>();
        player = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
        movementcontrollerplayer = player.GetComponent<MovementControllerPlayer>();

        // Set previous smooth time
        previousSmoothTime = movementcontrollerblock.velocitySmoothTime[2];
    }

    void Update(){
        if(triggered){
            // Destroy plane after desire speed is reached
            if(movementcontrollerblock.speed[2] < (speedAfterPlayerReached + 0.001f)){
                movementcontrollerblock.velocitySmoothTime[2] = previousSmoothTime;
                Destroy(gameObject);
            }
        }
    }
    
    void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("block")){
            //Debug.Log("Player reached!");
            movementcontrollerblock.velocitySmoothTime[2] = smoothTime;
            movementcontrollerblock.setSpeed[2] = speedAfterPlayerReached;
            triggered = true;

            // Set player to influence gravity and active
            player.isKinematic = false;
            player.useGravity = true;
            movementcontrollerplayer.isActive = true;
        }
    }
}