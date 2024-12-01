using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttributesManager : MonoBehaviour
{
    public int health;
    public int attack;

    public int armor;

    public float critDamage = 1.5f;
    public float critChance = 0.5f;
    public void takeDamage(int amount)
    {
        health -= amount - (amount * armor / 100);
    }
    public void DealDamage(GameObject target)
    {
        var atm = target.GetComponent<AttributesManager>();
        if (atm != null)
        {
            float totalDamage = attack;
            if (Random.Range(0f, 1f) < critChance)
                totalDamage *= critChance;
            atm.takeDamage((int)totalDamage);
        }

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}