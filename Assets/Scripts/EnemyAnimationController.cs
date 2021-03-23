using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    public bool isPlaying;
    public bool isArrive;
    public bool leftOrRight;
    private Animation anim;
    public GameObject turret;
    private BulletFireControll bulletFireControll;
    void Start(){
        bulletFireControll = turret.GetComponent<BulletFireControll>();
        anim = gameObject.GetComponent<Animation>();
    }

    public void AnimTriggers(AnimationEvent myEvent){
        // Enemy01 Arrive
        if(myEvent.stringParameter == "arrive"){

            // Choose by random left or right side
            if (Random.Range(0, 2) == 1) leftOrRight = true;

            if (leftOrRight){
                anim.Play("Enemy01_Animation_Arrive_Left");
                bulletFireControll.isActive = true;
            }
            else {
                anim.Play("Enemy01_Animation_Arrive_Right");
                bulletFireControll.isActive = true;
            }
        }

        // Enemy01 Arrive Close
        if(myEvent.stringParameter == "arriveCloseLeft"){
            anim.Play("Enemy01_Animation_Loop_Left");
        }
        if(myEvent.stringParameter == "arriveCloseRight"){
            anim.Play("Enemy01_Animation_Loop_Right");
        }

        // Enemy02 arrive straight
        if (myEvent.stringParameter == "enemy02ArriveStraight"){
            anim.Play("Enemy02_Animation_Circle_Left");
            bulletFireControll.isActive = true;
        }
    }
}