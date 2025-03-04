using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 5; // Enemy chịu được 5 viên đạn

    public void TakeDamage()
    {
        health--; // Giảm 1 máu khi bị bắn
        if (health <= 0)
        {
            Destroy(gameObject); // Hủy Enemy khi hết máu
        }
    }
}
