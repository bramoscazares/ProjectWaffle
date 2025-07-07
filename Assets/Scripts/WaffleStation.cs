using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaffleStation : MonoBehaviour
{

    PlayerController playerController;

    GameManager gameManager;

    [SerializeField] private AudioClip waffleSound;

    private AudioSource audioSource;
    private void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void HandleWaffleStationInteraction()
    {
        if (playerController.currentMeal == "Nothing")
        {
            // If the player has no meal, set the current meal to "Waffles"
            playerController.currentMeal = "Waffles";
            gameManager.UpdateHoldUI();
            // Play the waffle sound
            audioSource.PlayOneShot(waffleSound);
        }
        else
        {
            // If the player already has a meal, log a message
            Debug.Log("You already have a meal in your hands!");
        }
    }
}
