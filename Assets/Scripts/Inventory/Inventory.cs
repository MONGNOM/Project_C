using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    
    void Start()
    {
        InventoryUnActive();
    }

    public void InventoryActive()
    { 
        gameObject.SetActive(true);
    }

    public void InventoryUnActive()
    {
        gameObject.SetActive(false);
    }
}
