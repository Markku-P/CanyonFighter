using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHitController : MonoBehaviour
{
    public float health;
    public bool isSmoking;
    public bool isOnFire;
    public bool isExploded;
    public bool isGameOver;
    public bool anyKeyEnabled;
    public ParticleSystem hitParticles;
    public ParticleSystem smokeParticles;
    public ParticleSystem fireParticles;
    public ParticleSystem explosionParticles;
    private MeshRenderer render;
    private MovementControllerBlock movementcontrollerblock;
    private MovementControllerPlayer movementcontrollerplayer;
    private PlayerFireController playerFireController;
    private GameObject hud;
    private HudController hudController;

    void Start()
    {
        // Check if player health is not zero at startup
        if(health == 0) health = 100f;

        var turret = gameObject.transform.Find("Turret");
        playerFireController = turret.GetComponent<PlayerFireController>();

        hud = GameObject.FindWithTag("HudController");
        hudController = hud.GetComponent<HudController>();

        movementcontrollerplayer = GameObject.FindWithTag("Player").GetComponent<MovementControllerPlayer>();

        render = gameObject.GetComponentInChildren<MeshRenderer>();
    }

    void Update(){
        if (isGameOver){
            if (Input.anyKey){
                if (anyKeyEnabled){
                    // Restart level
                    RestartLevel();
                }
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("bullet")){
            ParticleSystem newHit = Instantiate(hitParticles);
            newHit.transform.position = other.gameObject.transform.position;
            newHit.transform.rotation = other.gameObject.transform.rotation;
            Destroy(other.gameObject);
            CheckPlayerHealth(other.gameObject);
        }

        else if (other.gameObject.CompareTag("Rock")){
            //Debug.Log(other.relativeVelocity.magnitude);
            isGameOver = true;
            PlayerGameOver();
        }

        else if (other.gameObject.CompareTag("RockLittle")){
            //Debug.Log(other.relativeVelocity.magnitude);
            if (other.relativeVelocity.magnitude > 1){
                isGameOver = true;
                PlayerGameOver();
            }
        }
    }

    void CheckPlayerHealth(GameObject other){

        float hitDamage = GameObject.Find(other.name).GetComponent<MovementControllerBullet>().hitDamage;

        // If hit damage value not found use default value
        if (hitDamage == 0){
            hitDamage = 1;
        }

        // Player takes damage
        health -= hitDamage;

        // Update HUD player health
        hudController.SetPlayerHealth(health);

        // If health below 3
        if (health < 3){
            if (!isSmoking){
            // Generate smoke particles
            smokeParticles.Play();
            isSmoking = true;
            }
        }

        // If health below 1
        if (health < 2){
            if (!isOnFire){
            // Generate fire particles
            fireParticles.Play();
            isOnFire = true;
            }
        }

        // If health below 1
        if (health < 1){
            if(!isGameOver){
                isGameOver = true;
                PlayerGameOver();
            }
        }
    }

    void PlayerGameOver(){
        // Update HUD player health to 0
        hudController.SetPlayerHealth(0);

        // Slowdown time
        Time.timeScale = 0.3f;

        // Stop player gun controller and movement controller
        playerFireController.isActive = false;
        movementcontrollerplayer.isActive = false;

        // Stop all players particle systems
        var particles = gameObject.GetComponentsInChildren<ParticleSystem>();
        foreach(ParticleSystem particle in particles){
            particle.Stop();
        }

        // Play explosion and hide player ship
        explosionParticles.Play();
        render.enabled = false;

        // Find all enemys and set guns inactive
        var enemys = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemys){
            var enemyShip = enemy.gameObject.transform.GetChild(0);

            // Inactive guns
            var turret01 = enemyShip.transform.Find("Turret01");
            var bulletFireControll = turret01.GetComponent<BulletFireControll>();
            bulletFireControll.isActive = false;

            // Slow enemy flying speed
            var anim = enemyShip.GetComponent<Animation>();
            anim.Stop();

            // Slow landscape speed
            movementcontrollerblock = GameObject.FindWithTag("movementcontrollerblock").GetComponent<MovementControllerBlock>();
            movementcontrollerblock.setSpeed[2] = 10f;

            // Show Gameover screen
            Invoke("ShowGameOverScreen", 1f);
        }
    }
    
    void ShowGameOverScreen(){
        hudController.ShowPressAnyKey();
        anyKeyEnabled = true;
    }

    void RestartLevel(){
        //Application.LoadLevel(Application.loadedLevel);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}