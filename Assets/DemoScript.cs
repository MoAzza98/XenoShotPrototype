using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoScript : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public ItemScript[] itemsToPickup;
    
    public void PickupItem(int id)
    {
        bool result = inventoryManager.AddItem(itemsToPickup[id]);
        if(result == true)
        {
            Debug.Log("Item was added");
        }
    }

    public void GetSelectedItem()
    {
        ItemScript recievedItem = inventoryManager.GetSelectedItem(false);
        if(recievedItem != null)
        {
            Debug.Log("Recieved item: " + recievedItem);
        }else
        {
            Debug.Log("No item recieved");
        }
    }

    public void UseSelectedItem()
    {
        ItemScript recievedItem = inventoryManager.GetSelectedItem(true);
        if (recievedItem != null)
        {
            Debug.Log("Used item: " + recievedItem);
        }
        else
        {
            Debug.Log("No item used");
        }
    }

}
