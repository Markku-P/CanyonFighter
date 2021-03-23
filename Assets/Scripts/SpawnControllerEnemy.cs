using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnControllerEnemy : MonoBehaviour
{

    public GameObject enemy01;
    public GameObject enemy02;
    public int counterEnemy;
    private int enemy01Offset;
    private int enemy02Offset;
    private bool changeStage;
    private bool isActiveStage01;
    private bool isActiveStage02;
    private bool isActiveStage03;
    private bool isActiveStage04;
    private GameObject enemyStartPlane;
    void Start()
    {
        //enemyStartPlane = GameObject.FindWithTag("enemystartplane");
        isActiveStage01 = false;
        isActiveStage02 = false;
        changeStage = true;
    }

    void Update()
    {
        if (counterEnemy <= 0){
            if (changeStage){
                StageManager();
            }
        }
    }

    void Stage01(){
        ResetOffsets();
        changeStage = false;
        // Spawn 3 enemy01
        Invoke("AddEnemy01", 2f);
        Invoke("AddEnemy01", 3f);
        Invoke("AddEnemy01", 4f);
    }
    void Stage02(){
        ResetOffsets();
        changeStage = false;
        // Spawn 4 enemy01
        Invoke("AddEnemy01", 0f);
        Invoke("AddEnemy01", 0.5f);
        Invoke("AddEnemy01", 3f);
        Invoke("AddEnemy01", 3.5f);
    }
    void Stage03(){
        ResetOffsets();
        changeStage = false;
        // Spawn 3 enemy02
        Invoke("AddEnemy02", 0f);
        Invoke("AddEnemy02", 0.5f);
        Invoke("AddEnemy02", 2f);
    }
    void Stage04(){
        ResetOffsets();
        changeStage = false;
        // Spawn 2 enemy01 and 2 enemy02
        Invoke("AddEnemy02", 0f);
        Invoke("AddEnemy01", 0.5f);
        Invoke("AddEnemy02", 3f);
        Invoke("AddEnemy01", 3.5f);
    }

    void ResetOffsets(){
        enemy01Offset = 0;
        enemy02Offset = 0; 
    }
    void AddEnemy01(){
        // Spawn new Enemy
        GameObject newEnemy = Instantiate(enemy01);
        newEnemy.transform.position = new Vector3(0f,-1f,enemy01Offset);
        enemy01Offset -= 14;
        counterEnemy++;
    }
        void AddEnemy02(){
        // Spawn new Enemy
        GameObject newEnemy = Instantiate(enemy02);
        newEnemy.transform.position = new Vector3(5f + enemy02Offset,1f,-100f);
        enemy02Offset -= 5;
        counterEnemy++;
    }

    public void EnemyDestroyed(){

        // Update enemy counter
        if (UpdateEnemyCounter() <= 0){
            counterEnemy = 0;
            changeStage = true;
        }
    }

    int UpdateEnemyCounter(){
        //int currentCounterEnemy = (GameObject.FindGameObjectsWithTag("Enemy").Length - 1);
        counterEnemy--;
        int currentCounterEnemy = counterEnemy;
        return currentCounterEnemy;
    }

    void StageManager(){
        // Change Stages
        if (isActiveStage01){
            isActiveStage01 = false;
            isActiveStage02 = true;
            Stage02();
        }
        else if (isActiveStage02){
            isActiveStage02 = false;
            isActiveStage03 = true;
            Stage03();
        }
        else if (isActiveStage03){
            isActiveStage03 = false;
            isActiveStage04 = true;
            Stage04();
        }
        else if (isActiveStage04){
            isActiveStage04 = false;
            //isActiveStage05 = true;
            //Stage05();
        }
        else{
            isActiveStage01 = true;
            Stage01();
        }
    }
}