using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnControllerBlock : MonoBehaviour
{
    public bool isActiveAtStart;
    public GameObject startPlane;
    public GameObject [] blockArray;
    public float blockSpawnOffset;
    private int blockIndex;
    private float newBlockOffsetZ;
    private float previousBlockOffsetZ;

    void Start(){   
        if (isActiveAtStart){
            SpawnBlock(null);
        }
        //Invoke("SpawnBlock", 0f);
        //InvokeRepeating("SpawnBlock", 0f, 2f);
    }

    public void SpawnBlock(GameObject previusBlock){
        // Limit block spawn offset to 0-10
        if (blockSpawnOffset < 0) blockSpawnOffset = 0;
        else if(blockSpawnOffset > 10) blockSpawnOffset = 10;

        // Spawn new block
        GameObject newBlock = Instantiate(blockArray[blockIndex]);

        // Check if first block
        if (previusBlock != null){

            // Get blocks z-axis size
            newBlockOffsetZ = newBlock.GetComponent<Collider>().bounds.size[2];
            previousBlockOffsetZ = previusBlock.GetComponent<Collider>().bounds.size[2];

            // Calculate Offset
            float offset = (previusBlock.transform.position.z - (previousBlockOffsetZ / 2) - (newBlockOffsetZ / 2) - blockSpawnOffset);

            // Make offset vector
            Vector3 offsetVector = new Vector3(0f, 0f, offset);

            // Set offset vector to new block
            newBlock.transform.position = offsetVector; 
        }
        else {
            newBlock.transform.position = startPlane.transform.position;
        }
        
        // Update block index
        if (blockIndex < (blockArray.Length -1)) blockIndex++;
        else blockIndex = 0;
    }
}