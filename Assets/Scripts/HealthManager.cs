using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;


public class HealthManager : MonoBehaviour
{



    public Text healthText;

    private bool isGameOver = false;

    public float health, maxHealth = 3f;
    void Start()
    {

        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

        updateHealth();
        healthCheck();
        if (isGameOver && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    private void TriggerGameOver()
    {
        isGameOver = true;

        Time.timeScale = 0f; // Freeze game
    }


    public void RestartGame()
    {

        Time.timeScale = 1f; // Resume the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload current scene



    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            TriggerGameOver();
            //Destroy(gameObject);
        }
    }

    void updateHealth()
    {
        healthText.text = "HP: " + health.ToString();
    }
    
    private void healthCheck()
    {
        if (health <= 0)
        {
            TriggerGameOver();
        }
    }

    
}
