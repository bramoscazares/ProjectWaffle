using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggsStation : MonoBehaviour
{
    PlayerController playerController;
    GameManager gameManager;

    [SerializeField] private AudioClip eggsSound;

    private AudioSource audioSource;

    void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HandleEggsStationInteraction()
    {
        if (playerController.currentMeal == "Nothing")
        {
            playerController.currentMeal = "Eggs";
            gameManager.UpdateHoldUI();
            // Play the eggs sound
            audioSource.PlayOneShot(eggsSound);
        }
        else
        {
            Debug.Log("You already have a meal in your hands!");
        }

    }
}
