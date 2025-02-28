using UnityEngine;
using TMPro;

public class FloatingDamage : MonoBehaviour
{
    public float moveSpeed = 2f; // Tốc độ di chuyển lên trên
    public float lifetime = 1f; // Thời gian tồn tại của Text
    public Vector3 offset = new Vector3(0, 2f, 0); // Vị trí xuất hiện trên đối tượng
    public Vector3 randomizeOffset = new Vector3(0.5f, 0, 0); // Dao động vị trí ngẫu nhiên

    public TMP_Text damageText; // Tham chiếu đến TMP_Text
    private Transform target; // Mục tiêu hiển thị sát thương

    /// <summary>
    /// Thiết lập thông tin sát thương và mục tiêu.
    /// </summary>
    /// <param name="damage">Sát thương cần hiển thị</param>
    /// <param name="targetTransform">Transform của mục tiêu</param>
    public void SetDamage(int damage, Transform targetTransform)
    {
        // Gán sát thương vào Text
        if (damageText != null)
        {
            damageText.text = damage.ToString(); // Hiển thị sát thương
        }
        else
        {
            Debug.LogError("TMP_Text is not assigned!");
        }

        // Gán mục tiêu
        target = targetTransform;

        // Tạo vị trí ban đầu gần con quái
        if (target != null)
        {
            Vector3 worldPosition = target.position + offset;
            worldPosition += new Vector3(
                Random.Range(-randomizeOffset.x, randomizeOffset.x),
                Random.Range(-randomizeOffset.y, randomizeOffset.y),
                Random.Range(-randomizeOffset.z, randomizeOffset.z)
            );
            transform.position = Camera.main.WorldToScreenPoint(worldPosition);
        }

        // Hủy đối tượng sau lifetime
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Di chuyển Text lên trên màn hình
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
    }
}
