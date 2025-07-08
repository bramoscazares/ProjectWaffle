using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public static OrderManager Instance;

    public List<string> currentOrders = new List<string>();
    public TextMeshProUGUI orderListText; // assign in Inspector

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddOrder(string order, string table)
    {
        currentOrders.Add("*" + order + " at " + table);
        UpdateUI();
    }

    public void RemoveOrder(string order, string table)
    {
        string formattedOrder = "*" + order + " at " + table;
        if (currentOrders.Contains(formattedOrder))
        {
            currentOrders.Remove(formattedOrder);
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        orderListText.text = "Orders:\n";
        foreach (string order in currentOrders)
        {
            orderListText.text += order + "\n";
        }
    }
}