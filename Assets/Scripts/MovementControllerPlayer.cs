using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControllerPlayer : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public float gravity;
    public Rigidbody player;
    public bool isGround;
    public bool isActive = false;

    void Start(){
        Physics.gravity = new Vector3(0, gravity, 0);
    }


    void FixedUpdate(){
        if (isActive){
            float moveHorizontal = Input.GetAxis ("Horizontal");
            float moveVertical = Input.GetAxis ("Vertical");

            Vector3 movement = new Vector3 (-moveHorizontal, 0.0f, -moveVertical);
            //Vector3 movement = new Vector3 (-moveHorizontal, moveVertical, 0.0f);

            // Check if player wants to jump
            if (Input.GetKey("space")){
                if (isGround){
                    isGround = false;
                    player.AddForce(new Vector3(0f, jumpForce, 0f),ForceMode.Impulse);
                }
            }

            // Move player
            //player.AddForce (movement * speed);
            //player.MovePosition(transform.position + movement * speed * Time.fixedDeltaTime);
            player.MovePosition(transform.position + movement * speed);

            Physics.gravity = new Vector3(0, gravity, 0); // Only for debug

            // Restrict player Z rotation to max +-40
            if (player.rotation.eulerAngles.z > 40 && player.rotation.eulerAngles.z < 180){
                player.transform.rotation = Quaternion.Euler(0f, 180f, 40f);
            }
            if (player.rotation.eulerAngles.z < 320 && player.rotation.eulerAngles.z > 180){
                player.transform.rotation = Quaternion.Euler(0f, 180f, 320f);
            }
            else{
                player.transform.rotation = Quaternion.Euler(
                    player.rotation.eulerAngles.x,
                    180f,
                    player.rotation.eulerAngles.z);
            }
        }
    }

    void OnTriggerEnter(Collider collider){
        if (collider.gameObject.CompareTag("block")){
            if (!isGround){
                // Enable player to jump
                isGround = true;
            }
        }
    }
}