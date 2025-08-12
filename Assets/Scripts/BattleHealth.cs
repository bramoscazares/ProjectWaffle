using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BattleHealth : MonoBehaviour
{
    public Image heart1;
    public Image heart2;
    public Image heart3;
    public GameObject gameOverPanel;
    public Text gameOverText;

    public GameObject HealthPanel;
    public Text healthText;

    private int heartCount = 3;
    private bool isGameOver = false;

    public float health, maxHealth = 3f;
    void Start()
    {
        
        Resethearts();
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        updateHealth();
        if (isGameOver && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    public void takeHeart()
    {
        heartCount--;
        Debug.Log("Strike added. Current strike count: " + heartCount);

        // Display strikes visually
        if (heartCount == 3 && heart3 != null) heart3.enabled = false;
        if (heartCount == 2 && heart2 != null) heart2.enabled = false;
        if (heartCount == 1 && heart1 != null) heart1.enabled = false;


        {
            //heart1.disable = true;
            TriggerGameOver();
        }
    }

    private void TriggerGameOver()
    {
        isGameOver = true;
        if (gameOverPanel != null) gameOverPanel.SetActive(true);

        Time.timeScale = 0f; // Freeze game
    }

    public void Resethearts()
    {
        heartCount = 3;
        health = maxHealth;
        isGameOver = false;

        if (heart1 != null) heart1.enabled = true;
        if (heart2 != null) heart2.enabled = true;
        if (heart3 != null) heart3.enabled = true;

    }

    public void RestartGame()
    {

        Resethearts();
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
    
}
