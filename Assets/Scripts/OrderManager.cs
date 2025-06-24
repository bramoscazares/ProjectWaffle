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

    public void AddOrder(string order)
    {
        currentOrders.Add(order);
        UpdateUI();
    }

    private void UpdateUI()
    {
        orderListText.text = "";
        foreach (string order in currentOrders)
        {
            orderListText.text += order + "\n";
        }
    }
}