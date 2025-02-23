using UnityEngine;

public class Gun : MonoBehaviour
{
    public ParticleSystem muzzleFlash; // Kéo Particle System vào đây trong Unity
    public GameObject bulletPrefab;  // Prefab viên đạn
    public Transform firePoint;      // Vị trí đầu nòng súng
    public Camera playerCamera;      // Camera của nhân vật
    public float bulletSpeed = 20f;  // Tốc độ viên đạn
    public Color bulletColor = Color.yellow; // Màu viên đạn

    void Update()
    {
        if (Input.GetButtonDown("Fire1")) // Nhấn chuột trái
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Lấy hướng bắn từ Camera đến giữa màn hình
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point; // Điểm va chạm với vật thể
        }
        else
        {
            targetPoint = ray.GetPoint(100f); // Nếu không trúng gì, bắn xa 100 đơn vị
        }

        // Tạo viên đạn và hướng nó đến mục tiêu
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Vector3 shootDirection = (targetPoint - firePoint.position).normalized;

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = shootDirection * bulletSpeed;
        }

        // Đổi màu viên đạn
        Renderer bulletRenderer = bullet.GetComponent<Renderer>();
        if (bulletRenderer != null)
        {
            bulletRenderer.material.color = bulletColor;
        }
        muzzleFlash.Play();
    }
}
