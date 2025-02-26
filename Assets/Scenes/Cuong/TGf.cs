using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TGf : MonoBehaviour
{
    public GameObject gun;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float fireRate = 1f;
    public float range = 10f;
    public LayerMask targetLayer;
    public AudioClip shootSound;

    private float fireCooldown = 0f;
    private Transform target;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(CheckForTarget());
    }

    private IEnumerator CheckForTarget()
    {
        while (true)
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, range, targetLayer);
            if (hits.Length > 0)
            {
                target = hits[0].transform;
            }
            else
            {
                target = null;
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void Update()
    {
        fireCooldown -= Time.deltaTime;

        if (target != null)
        {
            gun.transform.LookAt(new Vector3(target.position.x, gun.transform.position.y, target.position.z));

            if (fireCooldown <= 0f)
            {
                Fire();
                fireCooldown = 1f / fireRate;
            }
        }
    }

    private void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = firePoint.forward * 20f;

        if (audioSource && shootSound)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }
}
