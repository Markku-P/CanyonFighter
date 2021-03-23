using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudController : MonoBehaviour
{
    
    public float maxHealth;
    public int score;
    public float currentHealth;
    public Slider healthSlider;
    public Text playerScore;
    public Text pressAnyKey;
    private PlayerHitController playerHitController;
    private GameObject player;
    

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerHitController = player.GetComponent<PlayerHitController>();

        // Set health slider to player start health
        healthSlider.maxValue = playerHitController.health;
        healthSlider.value = playerHitController.health;

        // Set player score to 0
        score = 0;
    }

    public void SetPlayerHealth(float health){
        // Set health slider value to current player health
        healthSlider.value = health;
    }
    public void SetPlayerScore(int newScore = 1){

        score += newScore;
        // Set score value on hud
        playerScore.text = score.ToString();
    }

    public void ShowPressAnyKey(){
        pressAnyKey.enabled = true;
    }
}