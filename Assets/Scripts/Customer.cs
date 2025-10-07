using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Customer : MonoBehaviour
{

    PlayerController playerController;

    CustomerSpawner customerSpawner;
    private SpriteRenderer spriteRenderer;
    public float moveSpeed = 2f;
    public Table targetTable;
    private bool isSeated = false;
    private bool inQueue = false;
    private float queueWaitTime = 0f;
    private float orderWaitTime = 0f;
    private float mealWaitTime = 0f;
    private bool warnedLeavingSoon = false;

    [SerializeField] private float maxOrderWaitTime = 30f;
    [SerializeField] private float maxQueueWaitTime = 30f;
    [SerializeField] private float maxMealWaitTime = 30f;

    [Header("Order System")]
    public string[] possibleOrders = { "Waffles", "Eggs", "Bacon", "Juice" };
    public string currentOrder;
    private bool orderGiven = false;

    [Header("Audio")]
    [SerializeField] private AudioClip leaveSound;

    [SerializeField] private AudioClip orderSound;

    [SerializeField] private AudioClip serveSound;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        customerSpawner = GameObject.FindGameObjectWithTag("CustomerSpawner").GetComponent<CustomerSpawner>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        TryFindTable();
    }

    // Update is called once per frame
    void Update()
    {
        if (targetTable != null && !isSeated)
        {
            MoveToTable();
        }

        if (inQueue)
        {
            queueWaitTime += Time.deltaTime;

            // Gradually change color toward red based on wait time
            float t = Mathf.Clamp01(queueWaitTime / maxQueueWaitTime);
            spriteRenderer.color = Color.Lerp(Color.green, Color.red, t);

            // Play warning sound once when close to leaving
            if (!warnedLeavingSoon && queueWaitTime >= maxQueueWaitTime - 5f)
            {
                audioSource.PlayOneShot(leaveSound);
                Debug.Log("Customer is getting impatient and may leave soon.");
                warnedLeavingSoon = true;
            }

            if (queueWaitTime >= maxQueueWaitTime)
            {
                LeaveQueue();
            }
            CheckForTable();
        }

        if (isSeated && orderGiven)
        {
            mealWaitTime += Time.deltaTime;

            // Gradually change color toward red based on meal wait time
            float t = Mathf.Clamp01(mealWaitTime / maxMealWaitTime);
            spriteRenderer.color = Color.Lerp(Color.green, Color.red, t);

            // Play warning sound once when close to leaving
            if (!warnedLeavingSoon && mealWaitTime >= maxMealWaitTime - 5f)
            {
                audioSource.PlayOneShot(leaveSound);
                Debug.Log("Customer is getting impatient and may leave soon.");
                warnedLeavingSoon = true;
            }

            if (mealWaitTime >= maxMealWaitTime)
            {
                Debug.Log("Customer waited too long for their meal and left.");
                onCustomerDestroy();
            }
        }

        if (isSeated && !orderGiven)
        {
            orderWaitTime += Time.deltaTime;

            // Gradually change color toward red based on order wait time
            float t = Mathf.Clamp01(orderWaitTime / maxOrderWaitTime);
            spriteRenderer.color = Color.Lerp(Color.green, Color.red, t);

            // Play warning sound once when close to leaving
            if (!warnedLeavingSoon && orderWaitTime >= maxOrderWaitTime - 5f)
            {
                audioSource.PlayOneShot(leaveSound);
                Debug.Log("Customer is getting impatient and may leave soon.");
                warnedLeavingSoon = true;
            }

            if (orderWaitTime >= maxOrderWaitTime)
            {
                Debug.Log("Customer waited too long for you to take their order and left.");
                onCustomerDestroy();
            }
        }
    }

    public void TryFindTable()
    {
        Table table = TableManager.Instance.GetAvailableTable();
        if (table != null)
        {
            AssignTable(table);
        }
        else
        {

            JoinQueue();
        }
    }

    public void AssignTable(Table table)
    {
        targetTable = table;
        targetTable.isOccupied = true;
        isSeated = false;
        inQueue = false;
        warnedLeavingSoon = false; // Reset warning state
        spriteRenderer.color = Color.green;
        queueWaitTime = 0f; // Reset wait time
        CustomerQueueManager.Instance.RemoveFromQueue(this); // Tell the queue manager
    }

    public void MoveToQueueSpot(Vector3 queueSpot)
    {
        StartCoroutine(MoveToPosition(queueSpot));
    }

    private IEnumerator MoveToPosition(Vector3 destination)
    {
        while (Vector3.Distance(transform.position, destination) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = destination; // Snap into exact spot
    }
    void JoinQueue()
    {
        if (!inQueue)
        {
            inQueue = true;
            queueWaitTime = 0f;
            CustomerQueueManager.Instance.JoinQueue(this);
            Debug.Log("Customer joined the queue.");
            Rigidbody2D rb = GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.velocity = Vector2.zero;
                rb.bodyType = RigidbodyType2D.Static; // Freezes all movement and physics
            }
        }
    }

    void CheckForTable()
    {
        Table table = TableManager.Instance.GetAvailableTable();
        if (table != null)
        {
            AssignTable(table);
        }

    }

    public void LeaveQueue()
    {

        Debug.Log("Customer waited too long and left.");
        inQueue = false;
        CustomerQueueManager.Instance.RemoveFromQueue(this); // Tell the queue manager
        GameManager.Instance.LoseLife(); // Lose a life
        Destroy(gameObject); // Remove the customer from the scene
    }


    void MoveToTable()
    {
        Vector3 targetPosition = targetTable.seatPoint.position;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            SitDown();
        }
    }

    void SitDown()
    {
        isSeated = true;
        // Trigger sit animation or change sprite

        Debug.Log("Customer has sat down.");

        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Static; // Freezes all movement and physics
        }

        GenerateOrder();


    }

    void GenerateOrder()
    {
        currentOrder = possibleOrders[Random.Range(0, possibleOrders.Length)];
        orderGiven = false;
        Debug.Log("Customer ordered: " + currentOrder);
    }

    // Is used when player takes customers' order
    public void TakeOrder()
    {

        Debug.Log("Order taken: " + currentOrder + " at table: " + targetTable.name);

        audioSource.PlayOneShot(orderSound);

        OrderManager.Instance.AddOrder(currentOrder, targetTable.name);  // Adds order to visible list when taken

        spriteRenderer.color = Color.green; // Change color to indicate order taken

        orderGiven = true; // Mark that the order has been given

    }

    public void ServeMeal(string meal)
    {
        if (meal == currentOrder)
        {
            Debug.Log("Customer received their meal: " + meal);
            spriteRenderer.color = Color.green; // Change color to indicate meal served
            isSeated = false; // Customer is no longer seated
            orderGiven = false; // Reset order given status
            mealWaitTime = 0f; // Reset meal wait time
            OrderManager.Instance.RemoveOrder(currentOrder, targetTable.name); // Remove order from the list
            targetTable.isOccupied = false; // Mark the table as available again
            GameManager.Instance.UpdateScore(10); // Update score for serving the meal
            playerController.currentMeal = "Nothing"; // Reset player's current meal
            GameManager.Instance.UpdateHoldUI(); // Update the UI to reflect the empty meal
            StartCoroutine(DestroyAfterSound());
        }
        else if (meal == "Nothing")
        {
            Debug.Log("You aren't holding anything.");
        }
        else
        {
            Debug.Log("Incorrect meal served. Customer expected: " + currentOrder);
        }


    }

    private IEnumerator DestroyAfterSound()
    {
        if (serveSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(serveSound);
            yield return new WaitForSeconds(serveSound.length);
        }
        Destroy(gameObject);
    }

    public void HandleInteraction()
    {
        if (isSeated && !orderGiven)
        {

            TakeOrder();

        }
        else if (isSeated && orderGiven)
        {
            ServeMeal(playerController.currentMeal);
        }
        else if (!isSeated)
        {
            Debug.Log("Customer is not seated yet.");
        }
    }


    private void onCustomerDestroy()
    {
        GameManager.Instance.LoseLife(); // Lose a life
        // Initiate brawl by passing the customer's position to the spawner
        
        //customerSpawner.initiateBrawl(transform.position); // Record the customer's position for the brawl
        

        Destroy(gameObject); // Remove the customer from the scene


    }




}


