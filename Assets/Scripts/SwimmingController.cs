using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimmingController : MonoBehaviour
{
    public float swimSpeed = 5f;
    public float swimUpSpeed = 3f;
    public float waterDrag = 2f;
    public float gravityScale = 1f;

    private bool isSwimming = false;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isSwimming)
        {
            Swim();
        }
    }

    void Swim()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float upDown = 0f;

        if (Input.GetKey(KeyCode.Space))
        {
            upDown = swimUpSpeed;
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            upDown = -swimUpSpeed;
        }

        Vector3 swimDirection = new Vector3(horizontal, upDown, vertical);
        rb.velocity = swimDirection * swimSpeed;

        rb.drag = waterDrag; // Thêm lực cản nước để nhân vật không rơi nhanh xuống
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            Debug.Log("Entered Water"); // Kiểm tra xem nhân vật có thực sự vào nước không

            isSwimming = true;
            rb.useGravity = false; // Tắt trọng lực
            rb.velocity = Vector3.zero; // Reset tốc độ rơi
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            Debug.Log("Exited Water"); // Kiểm tra xem nhân vật có thực sự ra khỏi nước không

            isSwimming = false;
            rb.useGravity = true; // Bật trọng lực lại khi lên bờ
            rb.drag = 0; // Reset drag khi ra khỏi nước
        }
    }
}
