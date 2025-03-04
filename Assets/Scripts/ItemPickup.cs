using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemIn item;
    private bool isPlayerLooking = false; // Kiểm tra xem người chơi có đang nhìn vào item hay không

    void Update()
    {
        if (isPlayerLooking && Input.GetKeyDown(KeyCode.E))
        {
            Pickup();
        }
    }

    void Pickup()
    {
        Destroy(this.gameObject);
        InventoryManager.Instance.AddItem(item);
    }

    void OnMouseOver()
    {
        isPlayerLooking = true; // Khi chuột đang hover vào item
    }

    void OnMouseExit()
    {
        isPlayerLooking = false; // Khi chuột rời khỏi item
    }
}
