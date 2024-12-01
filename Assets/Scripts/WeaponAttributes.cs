using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WeaponAttributes : MonoBehaviour
{
    public AttributesManager atm;

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            other.GetComponent<AttributesManager>().takeDamage(atm.attack);
        }
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}