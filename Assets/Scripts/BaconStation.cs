using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BaconStation : MonoBehaviour
{
    PlayerController playerController;
    GameManager gameManager;

    [SerializeField] private AudioClip baconSound;

    private AudioSource audioSource;

    void Awake()
    {
        // Initialize references to PlayerController and GameManager
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void HandleBaconStationInteraction()
    {
        if (playerController.currentMeal == "Nothing")
        {
            // If the player has no meal, set the current meal to "Bacon"
            playerController.currentMeal = "Bacon";
            gameManager.UpdateHoldUI();
            // Play the bacon sound
            audioSource.PlayOneShot(baconSound);
        }
        else
        {
            // If the player already has a meal, log a message
            Debug.Log("You already have a meal in your hands!");
        }
    }
}