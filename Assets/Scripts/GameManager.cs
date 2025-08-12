using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    PlayerController playerController;

    CustomerSpawner customerSpawner;
    
    private bool isGameOver = false;

    private bool hardMode = false; // Set to true for hard mode, false for normal mode

    public int score = 0;

    [Header("Audio")]
    [SerializeField] private AudioClip gameOverSound;
    private AudioSource audioSource;

    [Header("Game UI")]
    [SerializeField]  public int lives = 3;
    public TextMeshProUGUI LivesText; // Assign in Inspector
    public TextMeshProUGUI hpText; // Assign in Inspector

    public GameObject gameOverUI;

    public TextMeshProUGUI holdingText; // Assign in Inspector

    public TextMeshProUGUI scoreText; // Assign in Inspector


    private void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        customerSpawner = GameObject.FindGameObjectWithTag("CustomerSpawner").GetComponent<CustomerSpawner>();
        audioSource = GetComponent<AudioSource>();
        gameOverUI.SetActive(false);
        Instance = this;
    }

    void Update()
    {
        // Allow restart with R key if game over
        if (isGameOver && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }

        if (hardMode == false && score >= 50)
        {
            hardMode = true;
            Debug.Log("Hard mode activated!");
            customerSpawner.IncreaseSpawnRate(); // Increase spawn rate for hard mode

        }

        if (hardMode == true && score >= 100)
        {
            hardMode = false; // Reset hard mode
            Debug.Log("Super Hard mode activated!");
            customerSpawner.IncreaseSpawnRate(); // Increase spawn rate for super hard mode
        }

    }
    public void LoseLife()
    {
        lives--;
        Debug.Log("Customer left! Lives remaining " + lives);
        UpdateLivesUI();

        if (lives <= 0)
        {
            GameOver();
        }
    }

    public void UpdateLivesUI()
    {
        if (LivesText != null)
        {
            LivesText.text = "Lives: " + lives;
        }
    }

    public void UpdateHoldUI()
    {
        if (holdingText != null)
        {
            holdingText.text = "Currently Holding: " + playerController.currentMeal;
        }
    }

    public void UpdateScore(int points)
    {
        score += points;
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over!");
        // Add game-over logic here
        isGameOver = true;
        gameOverUI.SetActive(true);
        audioSource.PlayOneShot(gameOverSound); // Play game over sound
        audioSource.Stop(); // Stop any other sounds
        Time.timeScale = 0f;
    }
    
    public void RestartGame()
    {
        Time.timeScale = 1f; // Resume time
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
