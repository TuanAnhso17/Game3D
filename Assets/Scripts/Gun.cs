using UnityEngine;

public class Gun : MonoBehaviour
{
    public ParticleSystem muzzleFlash;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public Camera playerCamera;
    public float bulletSpeed = 50f;
    public Color bulletColor = Color.yellow;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Lấy hướng từ camera đến giữa màn hình
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point; // Nếu trúng vật thể, đạn sẽ bay về đó
        }
        else
        {
            targetPoint = ray.GetPoint(100f); // Nếu không trúng gì, đạn bay xa 100 đơn vị
        }

        // Tính hướng từ firePoint đến targetPoint
        Vector3 shootDirection = (targetPoint - firePoint.position).normalized;

        // Tạo viên đạn
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.LookRotation(shootDirection));

        // Gán vận tốc cho viên đạn
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = shootDirection * bulletSpeed;
        }

        // Đổi màu viên đạn
        Renderer bulletRenderer = bullet.GetComponent<Renderer>();
        if (bulletRenderer != null && bulletRenderer.material.HasProperty("_Color"))
        {
            bulletRenderer.material.SetColor("_Color", bulletColor);
        }

        // Bật hiệu ứng nòng súng
        if (muzzleFlash != null)
        {
            muzzleFlash.Play();
        }

        // Hủy viên đạn sau 3 giây
        Destroy(bullet, 3f);
    }
}
