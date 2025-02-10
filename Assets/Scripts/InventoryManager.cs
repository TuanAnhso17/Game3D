using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Transform itemHolder;
    public GameObject itemPrefab;
    public static InventoryManager Instance { get; private set; }
    public List<ItemIn> items = new List<ItemIn>();

    public void DisplayInventory()
    {
        foreach(Transform item in itemHolder)
         Destroy(item.gameObject);
        foreach(ItemIn item in items)
        {
            GameObject obj = Instantiate(itemPrefab, itemHolder);
            TextMeshProUGUI itemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            Image itemImage = obj.transform.Find("ItemImage")
                .GetComponent<Image>();
            itemName.text = item.itemName;
            itemImage.sprite = item.image;
        }
    }
    private void Awake()
    {
        if (Instance != null || Instance != this)
        {
            Destroy(Instance);
        }
        Instance = this;
    }
    public void AddItem(ItemIn item)
    {
        items.Add(item);
        DisplayInventory();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
