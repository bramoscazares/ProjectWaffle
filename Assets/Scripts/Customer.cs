using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public float moveSpeed = 2f;
    private Table targetTable;
    private bool isSeated = false;
    private bool inQueue = false;
    private Vector3 targetPosition;
    private float queueWaitTime = 0f;
    [SerializeField] private float maxQueueWaitTime = 15f; 

    // Start is called before the first frame update
    void Start()
    {
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

        if (queueWaitTime >= maxQueueWaitTime)
        {
            LeaveQueue();
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
        targetPosition = targetTable.seatPoint.position;
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
        }
    }
    public void LeaveQueue()
{
    Debug.Log("Customer waited too long and left.");
    inQueue = false;
    CustomerQueueManager.Instance.RemoveFromQueue(this); // Tell the queue manager
    GameManager.Instance.LoseLife(); // Lose a life
    Destroy(gameObject); // Remove the customer
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
        // Trigger sit animation or change sprite if needed
        Debug.Log("Customer has sat down.");
    }


}
