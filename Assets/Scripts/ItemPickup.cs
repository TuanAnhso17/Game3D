using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemIn item;
    void OnMouseDown()
    {
        Pickup();    
    }
    void Pickup()
    {
        Destroy(this.gameObject);
        InventoryManager.Instance.AddItem(item);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
