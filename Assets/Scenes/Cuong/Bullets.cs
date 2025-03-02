using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    public AudioClip hitSound;
    public float damage = 10f;
    public float forceAmount = 500f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Heath playerHealth = other.GetComponent<Heath>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }

            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                /*Vector3 forceDirection = (other.transform.position - transform.position).normalized;*/
                Vector3 forceDirection = other.transform.position - transform.position;
                forceDirection.y = 0.5f;
                forceDirection = forceDirection.normalized;
                rb.AddForce(forceDirection * forceAmount, ForceMode.Impulse);
            }

            AudioSource.PlayClipAtPoint(hitSound, transform.position);
            Destroy(gameObject);
        }
    }
}
