using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleMove : MonoBehaviour {

    public float speedX;
    public float speedY;
    public float speedZ;
    public float rotationX;
    public float rotationY;
    public float rotationZ;
    public float sizeZ;
    public GameObject startPlane;
    public GameObject endPlane;
    public SpawnControllerBlock spawncontrollerblock;
    public MovementControllerBlock movementcontrollerblock;
    private bool newBlockTrigger = true;

    void Start()
    {
        spawncontrollerblock = GameObject.FindWithTag("spawncontrollerblock").GetComponent<SpawnControllerBlock>();
        movementcontrollerblock = GameObject.FindWithTag("movementcontrollerblock").GetComponent<MovementControllerBlock>();
        startPlane = GameObject.FindWithTag("startplane");
        endPlane = GameObject.FindWithTag("endplane");
        sizeZ = gameObject.GetComponent<Collider>().bounds.size[2];
        UpdateSpeed();
    }

    void Update(){
        // update speed variables
        UpdateSpeed();

    }

    void FixedUpdate()
    {
        transform.Translate(
            speedX * Time.deltaTime,
            speedY * Time.deltaTime,
            speedZ * Time.deltaTime
        );
        
        transform.rotation = movementcontrollerblock.transform.rotation;

        // Check if block is far enough to destroy
        if (gameObject.transform.position.z > (endPlane.transform.position.z - (sizeZ / 2 ))){
            Destroy(gameObject);
        }

        // Check if can spawn new block
        if (newBlockTrigger){
            if (gameObject.transform.position.z > startPlane.transform.position.z + (sizeZ * 1.5)){
                newBlockTrigger = false;
                spawncontrollerblock.SpawnBlock(gameObject);
            }
        }
    }

    void UpdateSpeed()
    {
        speedX = movementcontrollerblock.speed[0];
        speedY = movementcontrollerblock.speed[1];
        speedZ = movementcontrollerblock.speed[2];
    }

    /*
    void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("endplane"))
        {
            //Debug.Log("Trigger endplane!");
            Destroy(gameObject);
        }
    }
    */

    /*
    void OnTriggerExit(Collider other){
        if(other.gameObject.CompareTag("startplane")){
            //Debug.Log("Trigger startplane!");
            movementcontrollerblock.GetComponent<MovementControllerBlock>().SpawnBlock();
        }
    }
    */
}