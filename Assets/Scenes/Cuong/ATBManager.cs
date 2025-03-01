using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATBManager : MonoBehaviour
{
    public int health;
    public int Attack;
    public int armor;
    public float critDamage = 1.5f;
    public float critChance = 0.5f;
    public void TakeDamage(int amount)
    {
        health -= amount - (amount * armor / 100);

        if (gameObject.CompareTag("Enemy"))
        {
            if (health <= 0)
            {
                EnemyDie();
            }
        }
    }
    public void EnemyDie()
    {
        Debug.Log("Ke thu die");
        Animator animator = gameObject.transform.GetChild(0).GetComponent<Animator>();
        animator.SetBool("Isdead", true);
        Destroy(gameObject, 5f);
    }

    public void DealDamage(GameObject target)
    {
        var atm = target.GetComponent<ATBManager>();
        if (atm != null)
        {
            float totaldDamage = Attack;
            if (Random.Range(0f, 1f) < totaldDamage)
            {
                totaldDamage *= critDamage;
            }
            atm.TakeDamage((int)totaldDamage);
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
