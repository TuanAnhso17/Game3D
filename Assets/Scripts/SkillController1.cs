using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SkillController1 : MonoBehaviour
{
    public float speed = 10f; // Tốc độ di chuyển
    public float lifetime = 5f; // Thời gian tồn tại

    void Start()
    {
        Destroy(gameObject, lifetime); // Xóa skill sau thời gian tồn tại
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self); // Di chuyển về phía trước
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) // Kiểm tra va chạm với kẻ địch
        {
            // Gây sát thương hoặc hiệu ứng
            Debug.Log("Hit " + other.name);
            Destroy(gameObject); // Hủy skill
        }
    }
}
