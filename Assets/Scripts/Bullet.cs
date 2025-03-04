using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(); // Gọi hàm giảm máu trong Enemy
            }

            Destroy(gameObject); // Hủy viên đạn sau khi va chạm
        }
    }
}
