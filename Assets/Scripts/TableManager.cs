using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableManager : MonoBehaviour
{

 public static TableManager Instance;
    public List<Table> tables = new List<Table>();

    void Awake()
    {
        Instance = this;
    }

    public Table GetAvailableTable()
    {
        foreach (Table table in tables)
        {
            if (!table.isOccupied)
            {
                return table;
            }
        }
        return null;
    }
    
}