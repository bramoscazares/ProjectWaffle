using System.Collections.Generic;
using UnityEngine;

public class CustomerQueueManager : MonoBehaviour
{
    public static CustomerQueueManager Instance;

    public List<Transform> queuePositions = new List<Transform>();
    private Queue<Customer> waitingCustomers = new Queue<Customer>();

    void Awake()
    {
        Instance = this;
    }

    public void JoinQueue(Customer customer)
    {
        waitingCustomers.Enqueue(customer);
        UpdateQueuePositions();
    }

    public void LeaveQueue(Customer customer)
    {
        waitingCustomers = new Queue<Customer>(waitingCustomers);
        UpdateQueuePositions();
    }

    public void TrySeatNextCustomer()
    {
        if (waitingCustomers.Count == 0) return;

        Table table = TableManager.Instance.GetAvailableTable();
        if (table != null)
        {
            Customer nextCustomer = waitingCustomers.Dequeue();
            nextCustomer.AssignTable(table);
            UpdateQueuePositions();
        }
    }

    void UpdateQueuePositions()
    {
        int index = 0;
        foreach (Customer customer in waitingCustomers)
        {
            if (index < queuePositions.Count)
            {
                customer.MoveToQueueSpot(queuePositions[index].position);
                index++;
            }
        }
    }

    public void RemoveFromQueue(Customer customer)
{
    // Rebuild queue without the one who left
    Queue<Customer> updatedQueue = new Queue<Customer>();

    foreach (Customer c in waitingCustomers)
    {
        if (c != customer)
        {
            updatedQueue.Enqueue(c);
        }
    }

    waitingCustomers = updatedQueue;
    UpdateQueuePositions(); // Update spots for everyone else
}
    
    
    
    public int GetWaitingCustomerCount()
    {
        return waitingCustomers.Count;
    }

}