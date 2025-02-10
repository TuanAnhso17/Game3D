using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemIn", menuName = "Inventory/ItemIn")]
public class ItemIn : ScriptableObject
{
    public enum ItemType
    {
        Consumables, Weapons
    }
    public int id;
    public string itemName;
    public int value;
    public Sprite image;
    public ItemType itemType;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
