using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heath : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damagee)
    {
        currentHealth -= damagee;
        Debug.Log("Player nhận " + damagee + " sát thương. Máu còn: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player đã chết!");
        // Thêm hiệu ứng chết ở đây (ví dụ: animation, disable player)
    }

    internal void TakeDamage(float damage)
    {
        throw new NotImplementedException();
    }
}
