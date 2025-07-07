using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuiceStation : MonoBehaviour
{
    PlayerController playerController;
    GameManager gameManager;

    [SerializeField] private AudioClip juiceSound;

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

    public void HandleJuiceStationInteraction()
    {
        if (playerController.currentMeal == "Nothing")
        {
            // If the player has no meal, set the current meal to "Juice"
            playerController.currentMeal = "Juice";
            gameManager.UpdateHoldUI();
            // Play the juice sound
            audioSource.PlayOneShot(juiceSound);
        }
        else
        {
            // If the player already has a meal, log a message
            Debug.Log("You already have a meal in your hands!");
        }
    }
}
