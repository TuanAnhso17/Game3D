using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTester : MonoBehaviour
{
    public AttributesManager character_atm;
    public AttributesManager enemy_atm;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
            character_atm.DealDamage(enemy_atm.gameObject);
        if (Input.GetKeyDown(KeyCode.P))
            enemy_atm.DealDamage(character_atm.gameObject);
        if (Input.GetKeyDown(KeyCode.I))
            DamagePopUpGenerator.current.CreatePopUp(character_atm.transform.position,"chuoi");

    }
}
