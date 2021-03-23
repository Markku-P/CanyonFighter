using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitController : MonoBehaviour
{
    public float health;
    public ParticleSystem hitParticles;
    public ParticleSystem smokeParticles;
    public ParticleSystem fireParticles;
    public ParticleSystem explosionParticles;
    private SpawnControllerEnemy spawnControllerEnemy;
    private BulletFireControll bulletFireControll;
    private MeshRenderer render;
    public bool isSmoking;
    public bool isOnFire;
    public bool isExploded;
    private GameObject hud;
    private HudController hudController;

    void Start(){
        spawnControllerEnemy = GameObject.FindWithTag("spawncontrollerenemy").GetComponent<SpawnControllerEnemy>();
        var turret01 = gameObject.transform.Find("Turret01");
        bulletFireControll = turret01.GetComponent<BulletFireControll>();
        hud = GameObject.FindWithTag("HudController");
        hudController = hud.GetComponent<HudController>();
        render = gameObject.GetComponentInChildren<MeshRenderer>();
    }

    void OnTriggerEnter(Collider other){

        if(other.gameObject.CompareTag("PlayerBullet")){

            // Generate particles
            ParticleSystem newHit = Instantiate(hitParticles);
            newHit.transform.position = other.gameObject.transform.position;
            newHit.transform.rotation = other.gameObject.transform.rotation;

            // Check Health
            CheckEnemyHealth(other);
        }
    }

    void CheckEnemyHealth(Collider other){

        float hitDamage = GameObject.Find(other.name).GetComponent<MovementControllerBullet>().hitDamage;

        // If hit damage value not found use default value
        if (hitDamage == 0){
            hitDamage = 1;
        }
    
        // Destroy players bullet
        Destroy(other.gameObject);

        // Enemy takes damage
        health -= hitDamage;

        // Adds player score
        hudController.SetPlayerScore(1);

        // If health below 3
        if (health < 3){
            if (!isSmoking){
            // Generate smoke particles
            smokeParticles.Play();
            isSmoking = true;
            }
        }

        // If health below 1
        if (health < 1){
            if (!isOnFire){
            // Generate fire particles
            fireParticles.Play();
            isOnFire = true;
            }
        }

        // If Health below zero destroy enemyship
        if (health < 0){

            // Adds player score (destroy bonus)
            hudController.SetPlayerScore(10);

            // Destroy enemyship
            if (!isExploded){
                isExploded = true;
                spawnControllerEnemy.EnemyDestroyed();

                // Stop enemy from shooting
                bulletFireControll.isActive = false;

                // Hide Enemy ship
                render.enabled = false;

                explosionParticles.Play();
                Destroy(gameObject.transform.parent.gameObject, 0.5f);
            }
        }
    }   
}