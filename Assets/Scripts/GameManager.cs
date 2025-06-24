using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject gameOverUI;
    private bool isGameOver = false;

    [SerializeField]
    private int lives = 3;

    private void Awake()
    {
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
    }
    public void LoseLife()
    {
        lives--;
        Debug.Log("Customer left! Lives remaining " + lives);

        if (lives <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over!");
        // Add game-over logic here
        isGameOver = true;
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
    }
    
    public void RestartGame()
    {
        Time.timeScale = 1f; // Resume time
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
