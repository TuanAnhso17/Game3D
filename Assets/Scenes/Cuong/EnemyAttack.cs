using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int attackDamage = 10;
    public float knockbackForce = 500f;  // Lực đẩy khi đánh
    public AudioClip hitSound;           // Âm thanh khi đánh trúng
    private AudioSource audioSource;

    private void Start()
    {
        // Giả sử GameObject chứa script đã có AudioSource
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Gây sát thương cho Player
            Heath playerHealth = other.GetComponent<Heath>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
            }

            // Áp dụng knockback (văng ra) cho Player
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Tính hướng đẩy: từ vị trí enemy đến vị trí player
                Vector3 forceDirection = other.transform.position - transform.position;
                // Thêm một chút thành phần lên để tạo hiệu ứng văng
                forceDirection.y = 0.5f;
                forceDirection = forceDirection.normalized;
                rb.AddForce(forceDirection * knockbackForce, ForceMode.Impulse);
            }

            // Phát âm thanh khi đánh trúng
            if (audioSource && hitSound)
            {
                audioSource.PlayOneShot(hitSound);
            }
        }
    }
}
