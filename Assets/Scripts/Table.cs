using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{

    public bool isOccupied = false;
    public Transform seatPoint; // The position the customer moves to
    public void FreeTable()
{
    isOccupied = false;
    CustomerQueueManager.Instance.TrySeatNextCustomer();
}
    
}
