using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trashcan : MonoBehaviour
{
    PlayerController playerController;
    GameManager gameManager;

    [SerializeField] private AudioClip trashSound;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        audioSource = GetComponent<AudioSource>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void HandleTrashcanInteraction()
    {
        playerController.currentMeal = "Nothing"; // Reset player's current meal
        gameManager.UpdateHoldUI(); // Update the UI to reflect the empty meal
        audioSource.PlayOneShot(trashSound); // Play the trash sound
        Debug.Log("Meal discarded in trashcan.");
    }
}
