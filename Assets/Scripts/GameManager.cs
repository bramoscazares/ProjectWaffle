using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    private int lives = 3;

    private void Awake()
    {
        Instance = this;
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
    }
}
