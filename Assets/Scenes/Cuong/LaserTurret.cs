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

    [Header("Audio Settings")]
    public AudioSource audioSource;   // AudioSource gắn trên trụ laser
    public AudioClip fireSound;       // Âm thanh khi bắn laser (có thể cài chế độ Loop)

    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;

        // Nếu bạn sử dụng AudioSource trên cùng GameObject, hãy đảm bảo đã gán fireSound và cài đặt looping nếu cần.
        if (audioSource != null)
        {
            audioSource.loop = true; // Nếu bạn muốn âm thanh phát liên tục khi bắn
        }
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

        // Vẽ điểm đầu laser từ firePoint
        lineRenderer.SetPosition(0, firePoint.position);

        // Sử dụng Raycast từ firePoint theo hướng firePoint.forward
        Ray ray = new Ray(firePoint.position, firePoint.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, range, hitLayers))
        {
            // Vẽ tia laser kết thúc tại điểm va chạm
            lineRenderer.SetPosition(1, hit.point);

            // Nếu trúng Player, gây sát thương theo thời gian
            if (hit.collider.CompareTag("Player"))
            {
                Heath playerHealth = hit.collider.GetComponent<Heath>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damagePerSecond * Time.deltaTime);
                }
            }
        }
        else
        {
            // Nếu không trúng gì, vẽ tia laser thẳng ra xa
            lineRenderer.SetPosition(1, firePoint.position + firePoint.forward * range);
        }

        // Phát âm thanh nếu chưa phát
        if (audioSource != null && fireSound != null && !audioSource.isPlaying)
        {
            audioSource.clip = fireSound;
            audioSource.Play();
        }
    }

    void StopLaser()
    {
        lineRenderer.enabled = false;
        // Dừng âm thanh khi không bắn
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
