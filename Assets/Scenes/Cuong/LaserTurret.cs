using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaserTurret : MonoBehaviour
{
    [Header("References")]
    public Transform turretHead;      // Phần xoay của trụ (đầu nòng)
    public Transform firePoint;       // Vị trí bắt đầu tia laser
    public LayerMask hitLayers;       // Layer mà laser có thể va chạm (bao gồm Player)

    [Header("Laser Settings")]
    public float range = 15f;         // Tầm bắn laser
    public float damagePerSecond = 10f;  // Sát thương theo giây

    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }

    void Update()
    {
        // Tìm Player theo tag "Player"
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            float dist = Vector3.Distance(transform.position, player.transform.position);
            if (dist <= range)
            {
                // Xoay trụ ngay lập tức về phía Player (chỉ theo chiều ngang)
                Vector3 lookTarget = new Vector3(player.transform.position.x, turretHead.position.y, player.transform.position.z);
                turretHead.LookAt(lookTarget);

                FireLaser(player);
            }
            else
            {
                StopLaser();
            }
        }
        else
        {
            StopLaser();
        }
    }

    void FireLaser(GameObject player)
    {
        lineRenderer.enabled = true;
        // Vẽ điểm bắt đầu tia laser
        lineRenderer.SetPosition(0, firePoint.position);

        // Bắn Raycast từ firePoint theo hướng firePoint.forward
        Ray ray = new Ray(firePoint.position, firePoint.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, range, hitLayers))
        {
            // Vẽ tia laser kết thúc tại điểm va chạm
            lineRenderer.SetPosition(1, hit.point);

            // Nếu trúng Player thì gây sát thương
            if (hit.collider.CompareTag("Player"))
            {
                // Giả sử Player có script PlayerHealth để xử lý sát thương
                Heath playerHealth = hit.collider.GetComponent<Heath>();
                if (playerHealth != null)
                {
                    // Gây sát thương theo thời gian
                    playerHealth.TakeDamage(damagePerSecond * Time.deltaTime);
                }
            }
        }
        else
        {
            // Nếu không trúng gì, vẽ tia laser thẳng ra xa
            lineRenderer.SetPosition(1, firePoint.position + firePoint.forward * range);
        }
    }

    void StopLaser()
    {
        lineRenderer.enabled = false;
    }
}
