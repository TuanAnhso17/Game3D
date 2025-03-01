using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        // Kiểm tra xem viên đạn có trúng Enemy không
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Hủy đối tượng Enemy
            Destroy(collision.gameObject);

            // Hủy viên đạn
            Destroy(gameObject);
        }
    }
}
