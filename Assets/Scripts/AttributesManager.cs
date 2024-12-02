using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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
        if (gameObject.CompareTag("Enemy"))
        {
            Slider slider = gameObject.transform.GetChild(1).transform.GetChild(0).transform.GetComponent<Slider>();
            slider.value = health;
            if (health <=0 )
            {
                EnemyDie();
            }
        }
    }
    public void EnemyDie()
    {
        Debug.Log("ke thu da chet");
        Animator ani = gameObject.transform.GetChild(0).GetComponent<Animator>();
        ani.SetBool("isDead", true);
        Destroy(gameObject, 5f);
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