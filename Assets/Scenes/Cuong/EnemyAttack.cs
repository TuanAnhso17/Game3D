using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int attackDamage = 10;
    public AudioClip hitSound; // Âm thanh khi đánh trúng
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Lấy component PlayerHealth để gây sát thương
            Heath playerHealth = other.GetComponent<Heath>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
            }

            // Phát âm thanh khi đánh trúng
            if (audioSource && hitSound)
            {
                audioSource.PlayOneShot(hitSound);
            }
        }
    }
}
