using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{

    [SerializeField]
    private GameObject customerPrefab;

    [SerializeField]
    private float maxspawnTime;

    [SerializeField]
    private float minspawnTime;

    private float timeUntilSpawn;

    // Start is called before the first frame update
    void Awake()
    {
        SetTimeUntilSpawn();
    }

    void Update()
    {
        timeUntilSpawn -= Time.deltaTime;

        if (timeUntilSpawn <= 0)
        {
            if (CanSpawnCustomer())
            {
                Instantiate(customerPrefab, transform.position, Quaternion.identity);
            }

            SetTimeUntilSpawn();
        }

    }

    private void SetTimeUntilSpawn()
    {
        timeUntilSpawn = Random.Range(minspawnTime, maxspawnTime);
    }

    private bool CanSpawnCustomer()
    {
        var queueManager = CustomerQueueManager.Instance;

        // Safety check in case the manager hasn't initialized yet
        if (queueManager == null)
        {
            Debug.LogWarning("CustomerQueueManager is missing!");
            return false;
        }

        int queueSize = queueManager.GetWaitingCustomerCount();
        int maxQueue = queueManager.queuePositions.Count;

        return queueSize < maxQueue;
    }
    
    public void IncreaseSpawnRate()
    {
        if (maxspawnTime > 1f)
        {
            maxspawnTime -= 2f; // Decrease max spawn time to increase spawn rate
        }
        if (minspawnTime > 0.5f)
        {
            minspawnTime -= 2f; // Decrease min spawn time to increase spawn rate
        }
        SetTimeUntilSpawn(); // Reset the timer with the new spawn rates
    }

}
